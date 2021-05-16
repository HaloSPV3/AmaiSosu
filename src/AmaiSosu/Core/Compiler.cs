using System.Collections.Generic;
using System.IO;
using System.Linq;
using AmaiSosu.Common;
using static System.Environment;
using static AmaiSosu.Resources.FileNames;
using File = System.IO.File;
using Module = AmaiSosu.Common.Module;
using Paths = AmaiSosu.Common.Paths;
using SFX = HXE.SFX;

namespace AmaiSosu.Core
{
    public class Compiler : Module
    {
        private static string _binariesPath = string.Empty;
        public List<Package> _packages;

        public Compiler(string binariesPath, List<Package> packages)
        {
            _binariesPath = binariesPath;
            _packages = packages;
        }

        public Compiler(string binariesPath, List<Package> packages, Output output) : base(output)
        {
            _binariesPath = binariesPath;
            _packages = packages;
        }

        /// <inheritdoc />
        /// <returns>
        ///     False if:
        ///     Invalid OpenSauce binaries directory path.
        ///     - or -
        ///     Important OpenSauce binaries don't exist.
        ///     - or -
        ///     Kornner Studios or its contents do not exist.
        ///     - or -
        ///     Direct3D9 Extensions DXSetup is not present.
        /// </returns>
        public Verification Verify()
        {
            var dxrDir = Path.Combine(Paths.KStudios, OpenSauceDirectory, "dxredist");
            var dxrFiles = new List<string>
            {
                "dsetup.dll",
                "dsetup32.dll",
                "dxdllreg_x86.cab",
                "dxredist.zip",
                "dxsetup.exe",
                "dxupdate.cab",
                "Jun2010_d3dx9_43_x86.cab"
            };
            var osMainClientBinaries = new List<string>
            {
                /* variants of client dll */
                "dinput8.dll",
                "opensauce.dll", // another app will need to move or rename this
                Path.Combine("mods", "opensauce.dll") // needs a mod loader
            };
            var osMainBinaries = new List<string>
            {
                "CheApe.map",
                "CheApeDLLG.dll",
                "CheApeDLLS.dll",
                "CheApeDLLT.dll",
                "crashrpt_lang.ini",
                "CrashRpt1401.dll",
                "CrashSender1401.exe",
                "dbghelp.dll",
                "Halo1_CE_Readme.txt",
                "Halo1_CheApe_Readme.txt",
                "msvcp100.dll",
                "msvcp120.dll",
                "msvcr100.dll",
                "msvcr120.dll",
                "OpenSauceDedi.dll",
                "OS_Guerilla.exe",
                "OS_haloceded.exe",
                "OS_Sapien.exe",
                "OS_Settings.Editor.xml",
                "OS_Tool.exe"
            };

            if (!Directory.Exists(_binariesPath))
                return new Verification(false, "Selected directory for OpenSauce binaries doesn't exist.");

            if (!osMainClientBinaries.Any(bin => File.Exists(Path.Combine(_binariesPath, bin))))
                return new Verification(false, "OpenSauce client binary not found.");
            foreach (var bin in osMainBinaries)
            {
                if (!File.Exists(Path.Combine(_binariesPath, bin)))
                    return new Verification(false, $"OpenSauce main binary not found: {bin}");
            }

            if (!Directory.Exists(Paths.KStudios))
                return new Verification(false, $"Kornner Studios not found in \"{Paths.ProgData}\"");

            if (!Directory.Exists(dxrDir))
                return new Verification(false, $"Direct3D9 Extensions not found in {dxrDir}");
            foreach (var dxredist in dxrFiles)
            {
                if (!File.Exists(Path.Combine(dxrDir, dxredist)))
                    return new Verification(false, $"DirectX Redist file not found: {dxredist}");
            }

            if (!Directory.Exists(Path.Combine(Paths.KStudios, OpenSauceDirectory, OpenSauceIDE)))
                return new Verification(false, "OpenSauceIDE not found in '/Kornner Studios/OpenSauce/'" + NewLine
                                                + "Deleted by Install procedure?");

            return new Verification(true);
        }

        /// <summary>
        ///     Compiles the OpenSauce libraries to packages.
        /// </summary>
        /// <exception cref="OpenSauceException">
        ///     Invalid OpenSauce binaries directory path.
        ///     - or -
        ///     Important OpenSauce binaries don't exist.
        ///     - or -
        ///     Kornner Studios or its contents do not exist.
        ///     - or -
        ///     Direct3D9 Extensions DXSetup is not present.
        /// </exception>
        public void Compile()
        {
            /// Summary
            /// 1. VERIFY expected files/directories are present.
            /// 2. COMPILE packages from selected directories
            /// 3. COMPILE packages to SFX assembly.

            /**
             * 1. Verification
             */
            WriteInfo("Verifying expected files.");
            var state = Verify();

            if (!state.IsValid)
                WriteAndThrow(new OpenSauceException(state.Reason));

            /**
             * 2. Compilation - packages
             */
            WriteInfo("Attempting to copy files to package folder.");

            foreach (var package in _packages)
            {
                package.Compile();
            }

            WriteSuccess("OpenSauce has been successfully compiled to the package workspace.");

            /**
             * 3. Compilation - SFX
             */
            WriteInfo("Compiling packages to fancy, self-extracting application.");

            SFX.Compile(new SFX.Configuration
            {
                Source = Paths.TempDI,
                Target = new DirectoryInfo(CurrentDirectory)
            });
        }

        protected override string Identifier { get; } = "OpenSauce.Compile";
    }
}
