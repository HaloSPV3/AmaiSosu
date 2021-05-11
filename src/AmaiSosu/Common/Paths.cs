using static System.Environment;
using static System.IO.Path;
using System.IO;
using static AmaiSosu.Resources.FileNames;

namespace AmaiSosu.Common
{
    public static class Paths
    {
        public static readonly string ProgData = GetFolderPath(SpecialFolder.CommonApplicationData);
        public static readonly string KStudios = Combine(ProgData, OpenSauceDeveloper);
        public static readonly DirectoryInfo TempDI = new DirectoryInfo(GetTempPath()).CreateSubdirectory("AmaiSosu.tmp");
        public static readonly string Temp = TempDI.FullName;
    }
}
