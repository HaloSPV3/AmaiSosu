using System.Collections.Generic;
using System.IO;
using System.Linq;
using AmaiSosu.Common;
using static System.Environment;

namespace AmaiSosu.Compilation
{
    public class Compiler : Module
    {
        public const string LibPackage = "lib";
        public const string GuiPackage = "gui";
        private static string _binariesPath = string.Empty;
        private static readonly string _progDataPath = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), "Kornner Studios");
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

        public List<Package> Packages
        {
            get => _packages;
            set
            {
                if (value.SequenceEqual(_packages)) return;
                _packages = value;
            }
        }

        // create the packages. Pass the paths. For ProgDatam, grab the folder name (in Package class).
        public Package LibPak = new Package("lib", "", _progDataPath);
        public Package GuiPak = new Package("gui", "", _binariesPath);


        protected override string Identifier { get; } = "OpenSauce.Compile";
    }
}
