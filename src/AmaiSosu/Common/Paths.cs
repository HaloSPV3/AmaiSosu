using static System.Environment;
using static System.IO.Path;
using System.IO;
using static AmaiSosu.Resources.FileNames;

namespace AmaiSosu.Common
{
    public static class Paths
    {
        /// <summary>
        ///     %ProgramData%
        /// </summary>
        public static readonly string ProgData = GetFolderPath(SpecialFolder.CommonApplicationData);

        /// <summary>
        ///     %ProgramData%/Kornner Studios/
        /// </summary>
        public static readonly string KStudios = Combine(ProgData, OpenSauceDeveloper);

        /// <summary>
        ///     %temp%/AmaiSosu.tmp/
        /// </summary>
        public static readonly DirectoryInfo TempDI = new DirectoryInfo(GetTempPath()).CreateSubdirectory("AmaiSosu.tmp");

        /// <summary>
        ///     %temp%/AmaiSosu.tmp/
        /// </summary>
        public static readonly string Temp = TempDI.FullName;
    }
}
