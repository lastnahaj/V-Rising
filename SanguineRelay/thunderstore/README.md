# Thunderstore packaging

The package metadata is prepared but publishing is intentionally manual.

Before building a Thunderstore package, add an owner-supplied 256 x 256 PNG as
`thunderstore/icon.png`. No placeholder artwork is included.

Build the solution in Release configuration, then run the Thunderstore CLI from
this directory. The distributable plugin and its runtime dependencies must match
the contents produced by `scripts/package.ps1`.

Copyright (c) 2026 Shikaru x InfiniteGamingServers. All Rights Reserved.
