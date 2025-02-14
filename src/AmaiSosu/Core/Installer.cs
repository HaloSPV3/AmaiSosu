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
using static System.Environment;
using static System.Environment.SpecialFolder;
using static System.IO.Path;
using SFX = HXE.SFX;

namespace AmaiSosu.Core
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
            _hcePath = hcePath;
            _packages = packages;
        }

        public Installer(string hcePath, List<Package> packages, Output output) : base(output)
        {
            _hcePath = hcePath;
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

            if (!File.Exists(Combine(_hcePath, "haloce.exe")))
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
            /// 1. EXTRACT packages from the entry assembly (usually a SFX AmaiSosu.GUI)
            /// 2. VERIFY the packages were extracted properly.
            /// 3. INSTALL packages.
            /// 4. INSTALL Direct3D9 Extensions

            /**
             * 1. Extraction
             */
            WriteInfo("Extracting packages...");

            SFX.Extract(new SFX.Configuration
            {
                Target = new DirectoryInfo(
                    Paths.Temp)
            });

            /**
             * 2. Verification
             */
            WriteInfo("Verifying the OpenSauce installer.");
            var state = Verify();

            if (!state.IsValid)
                WriteAndThrow(new OpenSauceException(state.Reason));

            WriteSuccess("OpenSauce installer has been successfully verified.");

            /**
             * 4. Installation - packages
             */
            WriteInfo("Attempting to install OpenSauce to the filesystem.");

            foreach (var package in _packages)
                package.Install();

            WriteSuccess("OpenSauce has been successfully installed to the filesystem.");
        }
    }
}
