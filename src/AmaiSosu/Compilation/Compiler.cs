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
    public class Compiler : Module
    {
        public const string LibPackage = "lib";
        public const string GuiPackage = "gui";
        private readonly string _binariesPath;
        private readonly static string _progDataPath = Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), "Kornner Studios");
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

        public static List<Package> Packages = new List<Package> { LibPak,  };

        public static Package LibPak = new Package(archiveName: "lib", description: "", path: Path.GetDirectoryName(_progDataPath));
        public static Package GuiPak = new Package("gui", "", "");

        public static System.IO.Compression.ZipArchive LibZip;
        public static System.IO.Compression.ZipArchive GuiZip;

        protected override string Identifier { get; } = "OpenSauce.Compile";
    }
}
