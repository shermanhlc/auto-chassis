# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Planned]
### Additions
- **! PRIORITY !** Automatically make `.pts` files
- Integrate `.toml` for config
- Checks for toebox clearance
- Rotate firewall to use the same x-y-z directionals
- Rollcage based on shock mount

### Fixes
- Firewall when at an angle reduces the height of the sims
- Offset for suspension points not accounted for
- Consider the ass size
- Consider the tube length

### Minor Pieces
- `ManPage.Help()` should use `Printer` class

## [alpha 0.2.3] 2024-09-23
### Added
- `VERSION` file embedded within the binary
### Changed
- No longer a "live" program. Uses arguments from the command line instead as, as this will be more helpful in the case of a TUI
- `Firewall.cs` uses `config` instead of `const` variables
- Better constructor for `Firewall` class
### Fixed
- Cast error from `config.toml`

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
