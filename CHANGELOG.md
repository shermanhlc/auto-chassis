# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]
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
