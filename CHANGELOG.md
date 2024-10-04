# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Planned]
### Additions
- Checks for toebox clearance
- Rotate firewall to use the same x-y-z directionals
- Rollcage based on shock mount
- Define which variables (x, y, z) are up/down, left/right, front/back

### CHANGES NEEDED FOR CHASSIS DESIGN
- `Firewall.cs` does not check head clearance properly
- SIM length is not encountered for
- Tabs are not accounted for
- Toebox is oriented the wrong way

### Fixes
- Firewall when at an angle reduces the height of the sims
- Firewall top needs to be 6 above head from a plane between the bars, not 6in from the bar themselves
- Offset for suspension points not accounted for
- Consider the ass size (size of the driver's rear end increases their height, should measure from sitting would fix this)
- Consider the tube length
- `Parser.cs` should notify for failures (this has been changed and needs reconsidering)

### Minor Pieces
- `ManPage.Help()` should use `Printer` class
- `PTSBuilder.cs` does not clear a `.pts` file before appending new data to it

## [alpha 0.2.6] 2024-10-04
### Added
- `Firewall.cs` has a new function `AngleAdjustments()` that takes an angle in the config and rotates the firewall along the AR-AL tube
- New config option `firewall_angle`
### Changed
- Parsing doubles no longer subjected to only positive values
- Tests updated to account for changes to `Parser.cs`
### Fixed
- Angles are converted to radians prior to calculations
- All points are initialized with a default constructor instead of a `zero-zero` object
- All firewall points are set (mirrored) instead of being (0, 0, 0)
### Removed
- Tests for `Parser.cs` that included the `zeroPermitted` flag

## [alpha 0.2.5] 2024-09-29
### Added
- `PTSBuilder.cs` can now build a properly labeled `.pts` file
- new config option: outout file path
- `Program.cs` test ground now builds a `.pts` file

## [alpha 0.2.4] 2024-09-25
### Added
- `AutoChassis.Tests` project
- Unit and integration tests
### Changed
- `Parser.cs` now throws exceptions instead of returing possibly valid data as a "flag" for bad input
- `Input.cs` can handle exceptions being thrown gracefully 

## [alpha 0.2.3] 2024-09-25
### Added
- `VERSION` file embedded within the binary
- `IO` directory for related files
- Default constructors for necessary classes
### Changed
- No longer a "live" program. Uses arguments from the command line instead as, as this will be more helpful in the case of a TUI
- Major refactor:
    - use of `config.toml` values instead of local `const` variables
    - moved input-output `IO` namespace instead of lumping all input-output helpers within `Utilities`
### Fixed
- Cast error from `config.toml`
- Warnings for possible null values on non-null data types

## [alpha 0.2.2] 2024-09-18
### Added
- `config.toml` for const variables
- class `Config.cs` to check validity of the `config.toml`
- custom error types
### Changed
- shuffled file directory structure

## [alpha 0.2.1] 2024-08-25
### Added
- Shock mount points are saved as part of the toebox
### Fixed
- X values for opposite side are negative, when it should be Y values that are negative not X

## [alpha 0.2.0] 2024-08-25
### Added
- Toebox now created based on suspension points and height of S points on firewall

## [alpha 0.1.0] 2024-08-18
### Added
- Firewall calculations are possible (technically)

## Releases
[alpha 0.2.3]: https://github.com/shermanhlc/auto-chassis/releases/tag/v0.2.3-alpha
[alpha 0.0.1]: https://github.com/shermanhlc/auto-chassis/releases/tag/v0.1.0-alpha
