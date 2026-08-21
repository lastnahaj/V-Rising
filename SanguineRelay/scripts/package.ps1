param(
    [string] $Configuration = 'Release'
)

$ErrorActionPreference = 'Stop'

function Invoke-CheckedDotNet {
    param([Parameter(Mandatory)] [string[]] $Arguments)

    $output = & dotnet @Arguments 2>&1
    if ($LASTEXITCODE -eq 0) {
        return
    }

    $safeOutput = ($output | Out-String)
    foreach ($privateValue in $script:privatePathValues) {
        if (-not [string]::IsNullOrWhiteSpace($privateValue)) {
            $safeOutput = $safeOutput.Replace($privateValue, '<private-path>', [StringComparison]::OrdinalIgnoreCase)
        }
    }

    throw "dotnet $($Arguments[0]) failed.`n$safeOutput"
}

function Assert-NoPrivateContent {
    param(
        [Parameter(Mandatory)] [byte[]] $Bytes,
        [Parameter(Mandatory)] [string] $DisplayName,
        [switch] $ThirdPartyBinary
    )

    $ascii = [Text.Encoding]::ASCII.GetString($Bytes)
    $utf16 = [Text.Encoding]::Unicode.GetString($Bytes)
    foreach ($entry in $script:privacyPatterns.GetEnumerator()) {
        if ($entry.Key -eq 'personal username') {
            $usernameAscii = $ascii
            $usernameUtf16 = $utf16
            if ($ThirdPartyBinary) {
                $usernameAscii = $usernameAscii.Replace('LastNode', '', [StringComparison]::OrdinalIgnoreCase)
                $usernameUtf16 = $usernameUtf16.Replace('LastNode', '', [StringComparison]::OrdinalIgnoreCase)
            }
            elseif (-not $DisplayName.EndsWith('.dll', [StringComparison]::OrdinalIgnoreCase)) {
                $usernameAscii = $usernameAscii.Replace('lastnahaj', '', [StringComparison]::OrdinalIgnoreCase)
                $usernameUtf16 = $usernameUtf16.Replace('lastnahaj', '', [StringComparison]::OrdinalIgnoreCase)
            }

            if ($usernameAscii.IndexOf($entry.Value, [StringComparison]::OrdinalIgnoreCase) -ge 0 -or
                $usernameUtf16.IndexOf($entry.Value, [StringComparison]::OrdinalIgnoreCase) -ge 0) {
                throw "Release privacy check failed: $($entry.Key) was found in $DisplayName."
            }

            continue
        }

        if ($ascii.IndexOf($entry.Value, [StringComparison]::OrdinalIgnoreCase) -ge 0 -or
            $utf16.IndexOf($entry.Value, [StringComparison]::OrdinalIgnoreCase) -ge 0) {
            throw "Release privacy check failed: $($entry.Key) was found in $DisplayName."
        }
    }

    foreach ($secretPattern in $script:secretPatterns.GetEnumerator()) {
        if ([regex]::IsMatch($ascii, $secretPattern.Value, [Text.RegularExpressions.RegexOptions]::IgnoreCase -bor [Text.RegularExpressions.RegexOptions]::CultureInvariant) -or
            [regex]::IsMatch($utf16, $secretPattern.Value, [Text.RegularExpressions.RegexOptions]::IgnoreCase -bor [Text.RegularExpressions.RegexOptions]::CultureInvariant)) {
            throw "Release secret check failed: $($secretPattern.Key) was found in $DisplayName."
        }
    }
}

function Assert-SafeEntryName {
    param([Parameter(Mandatory)] [string] $EntryName)

    if ($EntryName -match '^[A-Za-z]:' -or $EntryName.StartsWith('/') -or $EntryName.StartsWith('\')) {
        throw "Archive entry uses an absolute path: $EntryName"
    }

    if (($EntryName -split '[/\\]') -contains '..') {
        throw "Archive entry contains path traversal: $EntryName"
    }
}

$root = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$userProfilePath = [Environment]::GetFolderPath([Environment+SpecialFolder]::UserProfile)
$output = Join-Path $root "src\SanguineRelay\bin\$Configuration\net6.0"
$artifacts = Join-Path $root 'artifacts'
$stage = Join-Path $artifacts 'SanguineRelay-v1.0.0'
$archive = "$stage.zip"
$plugin = Join-Path $stage 'BepInEx\plugins\SanguineRelay'
$documentation = Join-Path $stage 'docs'
$privatePathValues = @(
    $root,
    $userProfilePath,
    (Join-Path $root 'src'),
    (Join-Path $root 'src\SanguineRelay\obj'),
    (Join-Path $root 'src\SanguineRelay\bin'),
    $stage
)
$privacyPatterns = [ordered]@{
    'personal username' = 'LastN'
    'Windows user-profile path' = 'C:\Users\'
    'forward-slash user-profile path' = 'C:/Users/'
    'private documents path' = 'Documents\ChatGPT'
    'forward-slash private documents path' = 'Documents/ChatGPT'
    'resolved repository root' = $root
    'forward-slash repository root' = $root.Replace('\', '/')
    'resolved user profile' = $userProfilePath
    'forward-slash user profile' = $userProfilePath.Replace('\', '/')
    'absolute source path' = (Join-Path $root 'src')
    'absolute object path' = (Join-Path $root 'src\SanguineRelay\obj')
    'absolute binary path' = (Join-Path $root 'src\SanguineRelay\bin')
    'absolute artifact staging path' = $stage
}
$secretPatterns = [ordered]@{
    'authorization header' = '(?m)^[^\S\r\n]*Authorization[^\S\r\n]*:'
    'populated bot token setting' = '(?m)^[^\S\r\n]*BotToken[^\S\r\n]*=[^\S\r\n]*\S+'
    'Discord multifactor token' = '\bmfa\.[A-Za-z0-9_-]{20,}\b'
    'Discord token-shaped value' = '\b[A-Za-z0-9_-]{20,30}\.[A-Za-z0-9_-]{6}\.[A-Za-z0-9_-]{25,40}\b'
}
$env:DOTNET_CLI_HOME = Join-Path $root '.dotnet-home'
$env:NUGET_PACKAGES = Join-Path $root '.packages'
$env:DOTNET_NOLOGO = '1'

Invoke-CheckedDotNet -Arguments @('restore', (Join-Path $root 'SanguineRelay.sln'), '--locked-mode', '--configfile', (Join-Path $root 'NuGet.config'))
Invoke-CheckedDotNet -Arguments @('test', (Join-Path $root 'SanguineRelay.sln'), '-c', $Configuration, '--no-restore')

if (Test-Path -LiteralPath $stage) {
    Remove-Item -LiteralPath $stage -Recurse -Force
}

if (Test-Path -LiteralPath $archive) {
    Remove-Item -LiteralPath $archive -Force
}

New-Item -ItemType Directory -Force -Path $plugin, $documentation | Out-Null

$runtimeAssemblies = @(
    'SanguineRelay.dll',
    'Discord.Net.Core.dll',
    'Discord.Net.Rest.dll',
    'Discord.Net.WebSocket.dll',
    'Microsoft.Bcl.AsyncInterfaces.dll',
    'Newtonsoft.Json.dll',
    'System.Collections.Immutable.dll',
    'System.Interactive.Async.dll',
    'System.Linq.Async.dll'
)

foreach ($assembly in $runtimeAssemblies) {
    $source = Join-Path $output $assembly
    if (-not (Test-Path -LiteralPath $source)) {
        throw "A required build output is missing: $assembly"
    }

    Copy-Item -LiteralPath $source -Destination (Join-Path $plugin $assembly)
}

foreach ($document in @('README.md', 'CHANGELOG.md', 'COPYRIGHT', 'THIRD_PARTY_NOTICES.md', 'config-example.cfg')) {
    Copy-Item -LiteralPath (Join-Path $root $document) -Destination (Join-Path $stage $document)
}

Copy-Item -LiteralPath (Join-Path $root 'docs\GAME_HOOKS.md') -Destination (Join-Path $documentation 'GAME_HOOKS.md')

$forbiddenExtensions = @('.pdb', '.log', '.user', '.suo', '.tmp')
$forbiddenNames = @('.deps.json')
$forbiddenAssemblyPrefixes = @('BepInEx.', '0Harmony', 'Il2CppInterop.', 'ProjectM', 'Unity.', 'UnityEngine.')
foreach ($file in Get-ChildItem -LiteralPath $stage -Recurse -File) {
    $relative = [IO.Path]::GetRelativePath($stage, $file.FullName).Replace('\', '/')
    Assert-SafeEntryName $relative
    $hasForbiddenName = @($forbiddenNames | Where-Object { $file.Name.EndsWith($_, [StringComparison]::OrdinalIgnoreCase) }).Count -gt 0
    $hasForbiddenPrefix = @($forbiddenAssemblyPrefixes | Where-Object { $file.Name.StartsWith($_, [StringComparison]::OrdinalIgnoreCase) }).Count -gt 0
    if ($forbiddenExtensions -contains $file.Extension.ToLowerInvariant() -or $hasForbiddenName -or $hasForbiddenPrefix) {
        throw "A forbidden release file was staged: $relative"
    }

    $isThirdPartyBinary = $file.Extension.Equals('.dll', [StringComparison]::OrdinalIgnoreCase) -and
        -not $file.Name.Equals('SanguineRelay.dll', [StringComparison]::OrdinalIgnoreCase)
    Assert-NoPrivateContent -Bytes ([IO.File]::ReadAllBytes($file.FullName)) -DisplayName $relative -ThirdPartyBinary:$isThirdPartyBinary
}

Compress-Archive -Path (Join-Path $stage '*') -DestinationPath $archive -CompressionLevel Optimal

$expectedEntries = @(
    'BepInEx/plugins/SanguineRelay/Discord.Net.Core.dll',
    'BepInEx/plugins/SanguineRelay/Discord.Net.Rest.dll',
    'BepInEx/plugins/SanguineRelay/Discord.Net.WebSocket.dll',
    'BepInEx/plugins/SanguineRelay/Microsoft.Bcl.AsyncInterfaces.dll',
    'BepInEx/plugins/SanguineRelay/Newtonsoft.Json.dll',
    'BepInEx/plugins/SanguineRelay/SanguineRelay.dll',
    'BepInEx/plugins/SanguineRelay/System.Collections.Immutable.dll',
    'BepInEx/plugins/SanguineRelay/System.Interactive.Async.dll',
    'BepInEx/plugins/SanguineRelay/System.Linq.Async.dll',
    'CHANGELOG.md',
    'config-example.cfg',
    'COPYRIGHT',
    'docs/GAME_HOOKS.md',
    'README.md',
    'THIRD_PARTY_NOTICES.md'
)

$zip = [IO.Compression.ZipFile]::OpenRead($archive)
try {
    $actualEntries = @()
    foreach ($entry in $zip.Entries) {
        Assert-SafeEntryName $entry.FullName
        if ($entry.FullName.EndsWith('/')) {
            continue
        }

        $actualEntries += $entry.FullName
        $stream = $entry.Open()
        try {
            $memory = [IO.MemoryStream]::new()
            try {
                $stream.CopyTo($memory)
                $entryName = [IO.Path]::GetFileName($entry.FullName)
                $isThirdPartyBinary = $entryName.EndsWith('.dll', [StringComparison]::OrdinalIgnoreCase) -and
                    -not $entryName.Equals('SanguineRelay.dll', [StringComparison]::OrdinalIgnoreCase)
                Assert-NoPrivateContent -Bytes $memory.ToArray() -DisplayName $entry.FullName -ThirdPartyBinary:$isThirdPartyBinary
            }
            finally {
                $memory.Dispose()
            }
        }
        finally {
            $stream.Dispose()
        }
    }

    $difference = Compare-Object ($expectedEntries | Sort-Object) ($actualEntries | Sort-Object)
    if ($difference) {
        throw 'The release archive manifest does not match the approved file list.'
    }
}
finally {
    $zip.Dispose()
}

Write-Host 'Release privacy, secret and archive-integrity checks passed.'
Write-Host 'Created artifacts\SanguineRelay-v1.0.0.zip'
