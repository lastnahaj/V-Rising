param(
    [string] $Destination = (Join-Path $PSScriptRoot '..\.references\BepInEx')
)

$ErrorActionPreference = 'Stop'

function Assert-ArchiveHash {
    param(
        [Parameter(Mandatory)] [string] $Path,
        [Parameter(Mandatory)] [string] $Expected,
        [Parameter(Mandatory)] [string] $Name
    )

    $actual = (Get-FileHash -LiteralPath $Path -Algorithm SHA256).Hash
    if (-not $actual.Equals($Expected, [StringComparison]::OrdinalIgnoreCase)) {
        throw "$Name archive SHA-256 mismatch. Expected $Expected but received $actual."
    }
}

$version = '1.733.2'
$asset = "https://github.com/decaprime/VRising-Modding/releases/download/$version/BepInEx_V-Rising_Experimental_Dev_$version.zip"
$assetSha256 = '997CFFB97AD2D054D8199F1066C53B68D1ADADBB291069F3CDAA542624451A8E'
$work = Join-Path ([System.IO.Path]::GetTempPath()) "SanguineRelay-BepInEx-$version"
$archive = "$work.zip"
$referenceVersion = '1.1.12-r99041-b2'
$referenceAsset = "https://api.nuget.org/v3-flatcontainer/vampirereferenceassemblies/$referenceVersion/vampirereferenceassemblies.$referenceVersion.nupkg"
$referenceSha256 = 'F3B02C9C8C038C50CA3325237C026C673651E32B039197FB5F162A7264962455'
$referenceWork = Join-Path ([System.IO.Path]::GetTempPath()) "SanguineRelay-VRising-$referenceVersion"
$referenceArchive = "$referenceWork.zip"
$referenceDestination = Join-Path (Split-Path -Parent $Destination) 'VRising-1.1.12'

if (Test-Path -LiteralPath $work) {
    Remove-Item -LiteralPath $work -Recurse -Force
}

if (Test-Path -LiteralPath $referenceWork) {
    Remove-Item -LiteralPath $referenceWork -Recurse -Force
}

New-Item -ItemType Directory -Force -Path $work, $Destination, $referenceWork, $referenceDestination | Out-Null
Invoke-WebRequest -UseBasicParsing -Uri $asset -OutFile $archive
Assert-ArchiveHash -Path $archive -Expected $assetSha256 -Name "BepInEx $version"
Expand-Archive -LiteralPath $archive -DestinationPath $work -Force

$source = Join-Path $work 'BepInEx\core'
$assemblies = @(
    '0Harmony.dll',
    'BepInEx.Core.dll',
    'BepInEx.Unity.IL2CPP.dll',
    'Il2CppInterop.Common.dll',
    'Il2CppInterop.Runtime.dll'
)

foreach ($assembly in $assemblies) {
    Copy-Item -LiteralPath (Join-Path $source $assembly) -Destination (Join-Path $Destination $assembly) -Force
}

Invoke-WebRequest -UseBasicParsing -Uri $referenceAsset -OutFile $referenceArchive
Assert-ArchiveHash -Path $referenceArchive -Expected $referenceSha256 -Name "V Rising references $referenceVersion"
Expand-Archive -LiteralPath $referenceArchive -DestinationPath $referenceWork -Force
Copy-Item -Path (Join-Path $referenceWork 'ref\net6.0\*.dll') -Destination $referenceDestination -Force
Copy-Item -LiteralPath (Join-Path $referenceWork 'buildTransitive\interop\Il2CppInterop.Common.dll') -Destination (Join-Path $Destination 'Il2CppInterop.Common.dll') -Force
Copy-Item -LiteralPath (Join-Path $referenceWork 'buildTransitive\interop\Il2CppInterop.Runtime.dll') -Destination (Join-Path $Destination 'Il2CppInterop.Runtime.dll') -Force

Remove-Item -LiteralPath $work -Recurse -Force
Remove-Item -LiteralPath $archive -Force
Remove-Item -LiteralPath $referenceWork -Recurse -Force
Remove-Item -LiteralPath $referenceArchive -Force
Write-Host "BepInEx $version and V Rising 1.1.12 compile references installed."
