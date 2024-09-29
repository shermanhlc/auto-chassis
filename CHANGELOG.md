# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Planned]
### Additions
- **! PRIORITY !** Automatically make `.pts` files
- Checks for toebox clearance
- Rotate firewall to use the same x-y-z directionals
- Rollcage based on shock mount

### Fixes
- Firewall when at an angle reduces the height of the sims
- Offset for suspension points not accounted for
- Consider the ass size
- Consider the tube length
- `Parser.cs` should notify for failures

### Minor Pieces
- `ManPage.Help()` should use `Printer` class

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
[0.0.1]: https://github.com/shermanhlc/auto-chassis/releases/tag/v0.1.0-alpha
