/**
 * Copyright (C) 2018-2019 Emilian Roman
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

using System.Collections.Generic;
using System.IO;
using AmaiSosu.Common;
using HXE;
using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.Path;

namespace AmaiSosu.Installation
{
    /// <summary>
    ///     Type for installing OpenSauce to the file system.
    /// </summary>
    public class Installer : Module, IVerifiable
    {
        /// <summary>
        ///     Name of the OpenSauce core and dependencies package.
        /// </summary>
        public const string LibPackage = "lib";

        /// <summary>
        ///     Name of the in-game OpenSauce UI assets package.
        /// </summary>
        public const string GuiPackage = "gui";

        /// <summary>
        ///     Name of the OpenSauce XML user configuration package.
        /// </summary>
        public const string UsrPackage = "usr";

        private readonly string _hcePath;

        private readonly List<Package> _packages;

        public Installer(string hcePath, List<Package> packages)
        {
            _hcePath  = hcePath;
            _packages = packages;
        }

        public Installer(string hcePath, List<Package> packages, Output output) : base(output)
        {
            _hcePath  = hcePath;
            _packages = packages;
        }

        protected override string Identifier { get; } = "OpenSauce.Install";

        /// <inheritdoc />
        /// <returns>
        ///     False if:
        ///     Invalid HCE directory path.
        ///     - or -
        ///     Target directory does not exist.
        ///     - or -
        ///     Package does not exist.
        /// </returns>
        public Verification Verify()
        {
            if (!Directory.Exists(_hcePath))
                return new Verification(false, "Target directory for OpenSauce installation does not exist.");

            if (!System.IO.File.Exists(Combine(_hcePath, "haloce.exe")))
                return new Verification(false, "Invalid target HCE directory path for OpenSauce installation.");

            foreach (var package in _packages)
            {
                var packageState = package.Verify();
                if (!packageState.IsValid)
                    return new Verification(false, packageState.Reason);
            }

            return new Verification(true);
        }

        /// <summary>
        ///     Installs the OpenSauce libraries to the given HCE directory path.
        /// </summary>
        /// <exception cref="OpenSauceException">
        ///     Invalid HCE directory path.
        ///     - or -
        ///     Target directory does not exist.
        ///     - or -
        ///     Package does not exist.
        /// </exception>
        public void Install()
        {
            /// TODO
            /// Display HXE's console output in AmaiSosu.GUI.
            /// It needs to be copied to AmaiSosu.GUI AND a standard console
            ///  output so the output can still be displayed or caught if the
            ///  GUI is skipped.
            /// TODO Add checkbox (default: checked) to delete/cleanup extracted files afterward.
            /// TODO Backup Kornner Studios directory.
            
            /// 1. EXTRACT packages from the entry assembly (usually a SFX AmaiSosu.GUI)
            /// 2. VERIFY the packages were extracted properly.
            /// 3. Delete the Kornner Studios directory if it exists.
            /// 4. INSTALL packages.
            /// 5. INSTALL Direct3D9 Extensions

            /** 
             * 1. Extraction 
             */
            WriteInfo("Extracting packages...");
            
            SFX.Extract(new SFX.Configuration
            {
                Target = new DirectoryInfo(
                    Combine(CurrentDirectory, Package.Directory))
            });

            /**
             * 2. Verification
             */
            WriteInfo("Verifying the OpenSauce installer.");
            var state = Verify();

            if (!state.IsValid)
                WriteAndThrow(new OpenSauceException(state.Reason));

            /**
             * 3. Delete Kornner Studios 
             */
            var data = Combine(GetFolderPath(CommonApplicationData), "Kornner Studios");

            if (Directory.Exists(data))
            {
                try
                {
                    Directory.Delete(data, true);
                }
                catch (System.UnauthorizedAccessException)
                {
                    /// <see cref="https://stackoverflow.com/a/31363010/14894786"/>
                    /// <seealso cref="https://stackoverflow.com/a/8055390/14894786"/>
                    var batPath = Combine(CurrentDirectory, Package.Directory, "AdminDelKorn.bat");
                    var batText = "del /s /q \"Kornner Studios\" && rmdir /s /q \"Kornner Studios\"";
                    System.IO.File.WriteAllText(batPath, batText);
                    new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = batPath,
                        Verb = "RunAs",
                        WorkingDirectory = GetFolderPath(CommonApplicationData)
                    };
                }
            }

            WriteSuccess("OpenSauce installer has been successfully verified.");

            /**
             * 4. Installation - packages
             */
            WriteInfo("Attempting to install OpenSauce to the filesystem.");

            foreach (var package in _packages)
                package.Install();

            WriteSuccess("OpenSauce has been successfully installed to the filesystem.");

            /** 
             * 5. Installation - Direct3D9 Extensions 
             */
            WriteInfo("Direct3D9 Extensions must be installed for Open Sauce to work.");

            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = Combine(GetFolderPath(CommonApplicationData),
                                       "Kornner Studios",
                                       "OpenSauce",
                                       "dxredist",
                                       "dxsetup.exe"),
                    Verb = "RunAs",
                }
            };

            process.Start();
            process.WaitForExit();
            if (process.ExitCode != 0)
                WriteAndThrow(new System.Exception($"DirectX Setup exitted with code {process.ExitCode}."));
        }
    }
}
