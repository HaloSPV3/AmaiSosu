/**
 * Copyright (C) 2018-2019 Emilian Roman
 * Copyright (c) 2021 Noah Sherwin
 *
 * This file is part of AmaiSosu.
 *
 * AmaiSosu is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * AmaiSosu is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with AmaiSosu.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using AmaiSosu.Common;
using AmaiSosu.Common.IO;
using AmaiSosu.Installation;
using static System.Environment;
using Paths = AmaiSosu.Common.Paths;
using static AmaiSosu.Resources.FileNames;

namespace AmaiSosu
{
    public sealed partial class Main
    {
        /// <summary>
        ///     Invokes the compilation procedure.
        /// </summary>
        public void Compile()
        {
            CopyOSIDE();
            new PackageFactory(_path).GetCompiler().Compile();
            FinishCompile();
        }

        /// <summary>
        ///     If missing, copy the OpenSauceIDE from selected path to its home in Kornner Studios.<br/>
        ///     Needed by Install procedure.
        /// </summary>
        private void CopyOSIDE()
        {
            if (!Directory.Exists(Paths.KStudios))
                throw new OpenSauceException("Kornner Studios directory not found.");

            var local = Path.Combine(_path, OpenSauceIDE);
            var global = Path.Combine(Paths.KStudios, OpenSauceDirectory, OpenSauceIDE);

            if (!Directory.Exists(global) && Directory.Exists(local))
                Copy.All(local, global);
        }

        private string RenameProduct()
        {
            var exeFileInfo = new FileInfo(Assembly.GetEntryAssembly()?.Location
                             ?? throw new InvalidOperationException());

            var fvi = FileVersionInfo.GetVersionInfo(exeFileInfo.FullName);
            var product = new FileInfo(Path.Combine(CurrentDirectory, exeFileInfo.Name));
            var exeName = exeFileInfo.Name;

            exeName = exeName.Replace(".GUI.exe", $"-v{fvi.ProductVersion}.exe");

            var renamedProduct = Path.Combine(CurrentDirectory, exeName);

            foreach (var process in Process.GetProcessesByName(exeName))
            {
                process.Kill();
            }

            File.Delete(renamedProduct); // delete existing file
            product.MoveTo(renamedProduct);
            return renamedProduct;
        }

        /// <summary>
        ///     Conducts optional compilation finalisation routines.
        /// </summary>
        private void FinishCompile()
        {
            /// cleanup %temp%\\AmaiSosu.tmp\\
            Directory.Delete(Paths.Temp, true);

            /// modified from https://stackoverflow.com/a/9646139/14894786
            Process.Start("explorer.exe", "/select, " + RenameProduct());
        }
    }
}
