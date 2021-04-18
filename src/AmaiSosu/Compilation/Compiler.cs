using System;
using System.Collections.Generic;
using System.IO;
using HXE;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmaiSosu.Common;
using static System.Reflection.Assembly;
using static System.Environment;

namespace AmaiSosu.Compilation
{
    public class Compiler : Module, IVerifiable
    {
        public const string LibPackage = "lib";
        public const string GuiPackage = "gui";
        private readonly string _binariesPath;
        private readonly static string _progDataPath = System.IO.Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), "Kornner Studios");
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

        protected override string Identifier { get; } = "OpenSauce.Compile";

        public Verification Verify()
        {
            if (!Directory.Exists(_binariesPath))
                return new Verification(false, $"Could not find path: '{_progDataPath}'");

            if (!System.IO.File.Exists(Path.Combine(_binariesPath, "haloce.exe")))
                return new Verification(false, "");

            foreach (var package in _packages)
            {
                var packageState = package.Verify();
                if(packageState.IsValid)
                    return new Verification(false, packageState.Reason);
            }

            return new Verification(true);
        }

    }
}
