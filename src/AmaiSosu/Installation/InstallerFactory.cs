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

using System;
using System.Collections.Generic;
using System.IO;
using AmaiSosu.Common;
using AmaiSosu.Compilation;
using static AmaiSosu.Common.Paths;

namespace AmaiSosu.Installation
{
    public class InstallerFactory
    {
        /// <summary>
        ///     Available OpenSauce Installer types.
        /// </summary>
        public enum Type
        {
            CompilerDefault,
            InstallerDefault
        }

        private readonly string _installationPath;
        private readonly Output _output;
        private Type _selection;

        /// <summary>
        ///     OpenSauceInstallerFactory constructor.
        /// </summary>
        /// <param name="installationPath">
        ///     The HCE directory path -- used to install the OpenSauce library data to.
        /// </param>
        public InstallerFactory(string installationPath)
        {
            _installationPath = installationPath;
        }

        /// <inheritdoc />
        /// <param name="installationPath">
        ///     The HCE directory path -- used to install the OpenSauce library data to.
        /// </param>
        /// <param name="output">
        ///     Output class for packages to write messages to.
        /// </param>
        public InstallerFactory(string installationPath, Output output) : this(installationPath)
        {
            _output = output;
        }

        /// <summary>
        ///     Retrieves OpenSauceCompiler instance.
        /// </summary>
        /// <param name="type">
        ///     Type of OpenSauceCompiler instance.
        /// </param>
        /// <returns>
        ///     OpenSauceCompiler instancce for compiling OpenSauce to packages in an SFX application.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">type - null</exception>
        public Compiler GetCompiler(Type type = Type.CompilerDefault)
        {
            _selection = type;
            switch (type)
            {
                case Type.CompilerDefault:
                    return new Compiler(_installationPath, GetOpenSaucePackages(), _output);

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        ///     Retrieves OpenSauceInstaller instance.
        /// </summary>
        /// <param name="type">
        ///     Type of OpenSauceInstaller instance.
        /// </param>
        /// <returns>
        ///     OpenSauceInstaller instance for installing OpenSauce to the filesystem with the built-in packages.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Invalid OpenSauceInstaller type given.
        /// </exception>
        public Installer GetInstaller(Type type = Type.InstallerDefault)
        {
            _selection = type;
            switch (type)
            {
                case Type.InstallerDefault:
                    return new Installer(_installationPath, GetOpenSaucePackages(), _output);

                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        ///     Retrieve a list of packages that represent the OpenSauce installation data.
        /// </summary>
        /// <returns>
        ///     A list of OpenSauce packages that replicate an original OS installation when installed.
        ///     All of the packages are expected to be in the directory defined by the Package.Directory constant.
        /// </returns>
        private List<Package> GetOpenSaucePackages()
        {
            /// Install: new Package(source, desc, destination, _output)
            /// Compile: new Package(destination, desc, source, _output)
            var guiDirPath = ProgData;

            var libPackage = Path.Combine(Temp, Package.Directory, Installer.LibPackage);
            var guiPackage = Path.Combine(Temp, Package.Directory, Installer.GuiPackage);

            return new List<Package>
            {
                new Package(libPackage, "OpenSauce core and dependencies", _installationPath, _output),
                new Package(guiPackage, "In-game OpenSauce UI assets", guiDirPath, _output),
            };
        }
    }
}
