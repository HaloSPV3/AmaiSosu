using System.Collections.Generic;
using System.IO;
using System.Linq;
using AmaiSosu.Common;
using static System.Environment;

namespace AmaiSosu.Compilation
{
    public class Compiler : Module
    {
        private static string _binariesPath = string.Empty;
        private static readonly string _progData = GetFolderPath(SpecialFolder.CommonApplicationData);
        private static readonly string _kStudios = Path.Combine(_progData, "Kornner Studios");
        public const string LibPackage = "lib";
        public const string GuiPackage = "gui";
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
            var dxrDir = Path.Combine(_kStudios, "OpenSauce", "dxredist");
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
                "OS_Tool.exe",
                "vccorlib120.dll"
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

            if (!Directory.Exists(_kStudios))
                return new Verification(false, $"Kornner Studios not found in \"{_progData}\"");

            if (!Directory.Exists(dxrDir))
                return new Verification(false, $"Direct3D9 Extensions not found in {dxrDir}");
            foreach (var dxredist in dxrFiles)
            {
                if (!File.Exists(Path.Combine(dxrDir, dxredist)))
                    return new Verification(false, $"DirectX Redist file not found: {dxredist}");
            }

            if (!Directory.Exists(Path.Combine(_kStudios, "OpenSauce", "OpenSauceIDE")))
                return new Verification(false, "OpenSauceIDE not found in '/Kornner Studios/OpenSauce/'" + NewLine
                                                + "Deleted by Install procedure?");

            return new Verification(true);
        }

        /// <summary>
        ///     Compiles the OpenSauce libraries to packages.
        /// </summary>
        /// <exception cref="OpenSauceException">
        ///     Invalid
        /// </exception>
        public void Compile()
        {
            /// TODO need an Output instance from a Factory class.
            /// Summary
            /// 1. 
            _packages = new List<Package>{
                new Package("lib", "", _kStudios, output: null),
                new Package("gui", "", _binariesPath, output: null)
            };

            
            foreach(var package in _packages)
            {
                package.Compile();
            }
        }

        protected override string Identifier { get; } = "OpenSauce.Compile";
    }
}
