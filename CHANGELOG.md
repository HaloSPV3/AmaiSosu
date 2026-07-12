## [1.0.0-alpha.1](https://github.com/HaloSPV3/AmaiSosu/compare/v0.8.2...v1.0.0-alpha.1) (2026-07-12)

### ⚠ BREAKING CHANGES

* **deps,deps-dev:** multitarget net462, net480, net6.0-windows (Desktop)

### Features

* **deps,deps-dev:** multitarget net462, net480, net6.0-windows (Desktop) ([7ef3119](https://github.com/HaloSPV3/AmaiSosu/commit/7ef3119269458224dc8bcd16b4ccc5b694e1ad25))

### Bug Fixes

* **AmaiSosu, AmaiSosu.GUI, deps:** add `Meziantou.Polyfill` to use (some of) the latest .NET/C# features in older target frameworks; set `LangVersion` to `latest`; enable `Nullable` analysis ([4bdfb2c](https://github.com/HaloSPV3/AmaiSosu/commit/4bdfb2c868e158eef9130f3524daa4add96ee131))
* **AmaiSosu, AmaiSosu.GUI, deps:** downgrade `Microsoft.Windows.Compatibility` to a compatible release ([9b79e82](https://github.com/HaloSPV3/AmaiSosu/commit/9b79e8279b39f4cdf644171aef68f7ef34896f79))
* **AmaiSosu, AmaiSosu.GUI, deps:** replace `Version.txt` with `GitVersion.MsBuild` ([e73a7d5](https://github.com/HaloSPV3/AmaiSosu/commit/e73a7d5e1ab62d1788b7a552a8deb7c7501fa1d0))
* **AmaiSosu, deps:** drop redundant dependency `Microsoft.Windows.Compatibility` ([f12db1f](https://github.com/HaloSPV3/AmaiSosu/commit/f12db1f227f4c5fd843c0f3d4d5e4eef99b59d17))
* **AmaiSosu.GUI, Compile:** fail-over selected file's directory name to path root or empty string ([827ff2f](https://github.com/HaloSPV3/AmaiSosu/commit/827ff2f00bb944b411a1cce1615a81a9c58d22c8))
* **AmaiSosu.GUI, deps:** remove `Intern`; don't check access when we don't know what we need ([fe7cdbf](https://github.com/HaloSPV3/AmaiSosu/commit/fe7cdbf83fa86f1e0aa4771e5117fd80ddabb199))
* **AmaiSosu.GUI, Install:** call `OnPathChanged` after initializing `Install.Path` ([146049d](https://github.com/HaloSPV3/AmaiSosu/commit/146049d2b7d63a9167fce8f66ac5d422614d2f08))
* **AmaiSosu.GUI:** refactor removed `GlowBrush` to `GlowColor` ([1a7961e](https://github.com/HaloSPV3/AmaiSosu/commit/1a7961e17612c2f11362364d1ea01738b8cb8e04))
* **AmaiSosu.GUI:** remove dependency on unfinished "Intern" project ([b4d0748](https://github.com/HaloSPV3/AmaiSosu/commit/b4d07486197fb4dea747916ebbaa79abe681bc54))
* **AmaiSosu.GUI:** request running at "asInvoke" level instead of "requireAdministrator" ([c11e6c3](https://github.com/HaloSPV3/AmaiSosu/commit/c11e6c34efa03f4c17163e8106456716ad056f3a))
* **AmaiSosu.GUI:** un-enclose double-quoted CLI arguments ([f9eb39d](https://github.com/HaloSPV3/AmaiSosu/commit/f9eb39d7a1e66e890a1e45260a6b088c1e6d9c6a))
* **AmaiSosu:** initialize AmaiSosu.Startup.Path to `string.Empty` instead of `null` ([d0f4770](https://github.com/HaloSPV3/AmaiSosu/commit/d0f47706fbb8fcc2b032d421b436c596f1e20c66))
* **Common:** initialize `Verification.Reason` to empty string ([8d4541e](https://github.com/HaloSPV3/AmaiSosu/commit/8d4541ecd64f7f0421aa89fdda5eb10fbb26d3e4))
* **deps:** update HXE to 2.3.1; this trims a lot of transitive dependencies and loose DLLs ([1cacdb7](https://github.com/HaloSPV3/AmaiSosu/commit/1cacdb79b56d888430913714560e62425282da4e))
* **deps:** upgrade `Costura.Fody`, `MahApps.Metro` to 6.2.0, 2.4.11, respectively ([b72a80e](https://github.com/HaloSPV3/AmaiSosu/commit/b72a80ec73617adf5f1793515df712faad0f2ce7))

## [1.0.0-alpha.1](https://github.com/HaloSPV3/AmaiSosu/compare/v0.8.2...v1.0.0-alpha.1) (2026-07-11)

### ⚠ BREAKING CHANGES

* **deps,deps-dev:** multitarget net462, net480, net6.0-windows (Desktop)

### Features

* **deps,deps-dev:** multitarget net462, net480, net6.0-windows (Desktop) ([7ef3119](https://github.com/HaloSPV3/AmaiSosu/commit/7ef3119269458224dc8bcd16b4ccc5b694e1ad25))

### Bug Fixes

* **AmaiSosu, AmaiSosu.GUI, deps:** add `Meziantou.Polyfill` to use (some of) the latest .NET/C# features in older target frameworks; set `LangVersion` to `latest`; enable `Nullable` analysis ([4bdfb2c](https://github.com/HaloSPV3/AmaiSosu/commit/4bdfb2c868e158eef9130f3524daa4add96ee131))
* **AmaiSosu, AmaiSosu.GUI, deps:** downgrade `Microsoft.Windows.Compatibility` to a compatible release ([9b79e82](https://github.com/HaloSPV3/AmaiSosu/commit/9b79e8279b39f4cdf644171aef68f7ef34896f79))
* **AmaiSosu, AmaiSosu.GUI, deps:** replace `Version.txt` with `GitVersion.MsBuild` ([e73a7d5](https://github.com/HaloSPV3/AmaiSosu/commit/e73a7d5e1ab62d1788b7a552a8deb7c7501fa1d0))
* **AmaiSosu, deps:** drop redundant dependency `Microsoft.Windows.Compatibility` ([f12db1f](https://github.com/HaloSPV3/AmaiSosu/commit/f12db1f227f4c5fd843c0f3d4d5e4eef99b59d17))
* **AmaiSosu.GUI, Compile:** fail-over selected file's directory name to path root or empty string ([827ff2f](https://github.com/HaloSPV3/AmaiSosu/commit/827ff2f00bb944b411a1cce1615a81a9c58d22c8))
* **AmaiSosu.GUI, deps:** remove `Intern`; don't check access when we don't know what we need ([fe7cdbf](https://github.com/HaloSPV3/AmaiSosu/commit/fe7cdbf83fa86f1e0aa4771e5117fd80ddabb199))
* **AmaiSosu.GUI, Install:** call `OnPathChanged` after initializing `Install.Path` ([146049d](https://github.com/HaloSPV3/AmaiSosu/commit/146049d2b7d63a9167fce8f66ac5d422614d2f08))
* **AmaiSosu.GUI:** refactor removed `GlowBrush` to `GlowColor` ([1a7961e](https://github.com/HaloSPV3/AmaiSosu/commit/1a7961e17612c2f11362364d1ea01738b8cb8e04))
* **AmaiSosu.GUI:** remove dependency on unfinished "Intern" project ([b4d0748](https://github.com/HaloSPV3/AmaiSosu/commit/b4d07486197fb4dea747916ebbaa79abe681bc54))
* **AmaiSosu.GUI:** request running at "asInvoke" level instead of "requireAdministrator" ([c11e6c3](https://github.com/HaloSPV3/AmaiSosu/commit/c11e6c34efa03f4c17163e8106456716ad056f3a))
* **AmaiSosu.GUI:** un-enclose double-quoted CLI arguments ([f9eb39d](https://github.com/HaloSPV3/AmaiSosu/commit/f9eb39d7a1e66e890a1e45260a6b088c1e6d9c6a))
* **AmaiSosu:** initialize AmaiSosu.Startup.Path to `string.Empty` instead of `null` ([d0f4770](https://github.com/HaloSPV3/AmaiSosu/commit/d0f47706fbb8fcc2b032d421b436c596f1e20c66))
* **Common:** initialize `Verification.Reason` to empty string ([8d4541e](https://github.com/HaloSPV3/AmaiSosu/commit/8d4541ecd64f7f0421aa89fdda5eb10fbb26d3e4))
* **deps:** update HXE to 2.3.1; this trims a lot of transitive dependencies and loose DLLs ([1cacdb7](https://github.com/HaloSPV3/AmaiSosu/commit/1cacdb79b56d888430913714560e62425282da4e))
* **deps:** upgrade `Costura.Fody`, `MahApps.Metro` to 6.2.0, 2.4.11, respectively ([b72a80e](https://github.com/HaloSPV3/AmaiSosu/commit/b72a80ec73617adf5f1793515df712faad0f2ce7))

# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## v0.4.1
### Added
- Added --auto parameter to install to Working Directory.

## v0.4.0
### Added
- A completely new user interface and branding style.

### Removed
- Installation of user XML settings (OpenSauce generates them on start-up).

## v0.3.5
### Added
- Asynchronous invocation of the installation procedure.
- Ability to detect an officially SPV3.2 directory.
- Remove OpenSauce data directory pre-installation.

### Changed
- Changed licence to GPL-3.0.

## v0.3.4
### Added
- Manifest file for reinforcing administrative requirements.

### Fixed
- Eliminate the "This program might not have installed correctly" warning by adding a manifest file.

### Removed
- Atarashii library is no longer used; instead, its OpenSauce installation code has been ported to AmaiSosu.

## v0.3.3
### Changed
- The background image's resolution has been increased in size.
- Distributed SFX compilation method (from WinRAR to 7-Zip).

### Fixed
- False positives from some AVs on VirusTotal, due to the SFX compilation method. Cheers to moiler  & `Anton#6293` for
  informing me about the false positives.
- Graphical glitches on WINE due to previously lower resolution background. Cheers to
  [Lionir](https://github.com/lionirdeadman) for the heads up.

## v0.3.2
### Added
- Outputting of the source version/revision used for compiling the binary.
- Deactivation of the install button until a valid HCE directory is chosen.

## v0.3.1
### Fixed
- Unintentional backing up of default HCE shader files when installing OpenSauce. AmaiSosu now distinguishes betweeen
  OpenSauce shader files and HCE shader files. A thank you to the following invaluable testers for assisting with this
  issue: `Anton#6293`, [Michelle](https://github.com/gbMichelle) and [Lionir](https://github.com/lionirdeadman).

## v0.3.0
### Added
- Introduced support for installing the OpenSauce IDE to a dedicated sub-directory in the HCE directory, rather than to
  the original directory which is pretty obscure and inconvenient to access.

## v0.2.0
### Added
- Introduced support for detecting & backing up HAC2. Because HAC2 is incompatible with OpenSauce, AmaiSosu will back up
   its DLL to disable HCE from loading it.

## v0.1.0
### Added
- Everything! This is the initial release of AmaiSosu.
  Please report issues [here](https://www.reddit.com/r/halospv3/comments/9xvnn5/amaisosu_an_opensauce_installer/)!
