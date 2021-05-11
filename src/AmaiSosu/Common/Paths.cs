using static System.Environment;
using static System.IO.Path;
using static AmaiSosu.Resources.FileNames;

namespace AmaiSosu.Common
{
    public static class Paths
    {
        public static readonly string ProgData = GetFolderPath(SpecialFolder.CommonApplicationData);
        public static readonly string KStudios = Combine(ProgData, OpenSauceDeveloper);
        public static readonly string Temp = GetTempPath();
    }
}
