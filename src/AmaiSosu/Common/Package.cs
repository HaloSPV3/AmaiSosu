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

using System.IO;
using System.IO.Compression;
using AmaiSosu.Common.Exceptions;

namespace AmaiSosu.Common
{
    /// <summary>
    ///     Archive installer and verifier.
    /// </summary>
    public class Package : Module, IVerifiable
    {
        /// <summary>
        ///     Whether or not the parent/root directory of a package is preserved. <br/>
        ///     Useful for the 'Kornner Studios' directory.
        /// </summary>
        private bool _includeBaseDirectory;

        /// <summary>
        ///     Directory containing the expected packages.
        /// </summary>
        public const string Directory = "Packages";

        /// <summary>
        ///     Package archive extension.
        /// </summary>
        public const string Extension = "pkg";

        /// <summary>
        ///     Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="archiveName">Name of the archive.</param>
        /// <param name="description">The description.</param>
        /// <param name="path">The source or destination path.</param>
        /// <remarks>
        ///     Remember to get the parent directory when needed.
        /// </remarks>
        public Package(string archiveName, string description, string path, bool includeBaseDirectory = false)
        {
            ArchiveName = archiveName;
            Description = description;
            Path = path;
            _includeBaseDirectory = includeBaseDirectory;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Package"/> class.
        /// </summary>
        /// <param name="archiveName">Name of the archive.</param>
        /// <param name="description">The description.</param>
        /// <param name="path">The source or destination path.</param>
        /// <param name="output">The instance of Output for outputting inbound messages.</param>
        public Package(string archiveName, string description, string path, Output output)
            : base(output)
        {
            ArchiveName = archiveName;
            Description = description;
            Path = path;
        }

        protected override string Identifier { get; } = "Atarashii.Package";

        /// <summary>
        ///     Name of the archive file without any extensions or paths.
        /// </summary>
        public string ArchiveName { get; }

        /// <summary>
        ///     Informative line about the package.
        /// </summary>
        public string Description { get; }

        /// <summary>
        ///     Source or destination directory path for the installed contents.
        /// </summary>
        public string Path { get; }

        /// <inheritdoc />
        /// False if:
        /// - Package archive does not exist.
        /// - Install destination does not exist.
        public Verification Verify()
        {
            if (!System.IO.Directory.Exists(ArchiveName))
                return new Verification(false, "Cannot install specified package. Package's directory does not exist.");

            if (!System.IO.Directory.Exists(Path))
                return new Verification(false, "Cannot install specified package. Destination does not exist.");

            return new Verification(true);
        }

        /// <summary>
        ///     Compiles a directory to a new Package instance.
        /// </summary>
        /// TODO Instead of multiple packages, compile to just one package with
        /// contents' destinations determined at compile time.
        public void Compile()
        {
            try
            {
                ZipFile.CreateFromDirectory(Path, ArchiveName, CompressionLevel.Optimal, _includeBaseDirectory);
            }
            catch(IOException)
            {
                WriteWarn($"{Description} failed to compress to an archive.");
            }

            WriteInfo($"Verifying {Description} package.");
            var state = Verify();

            if (!state.IsValid)
                WriteAndThrow(new PackageException(state.Reason));

            WriteSuccess($"Package {Description} has been successfully verified.");
        }

        /// <summary>
        ///     Applies the files in the package to the destination on the filesystem.
        /// </summary>
        /// <exception cref="PackageException">
        ///     Package archive does not exist.
        ///     - or -
        ///     Destination directory does not exist.
        /// </exception>
        /// TODO Modify for HXE-SFX
        public void Install()
        {
            WriteInfo($"Verifying {Description} package.");
            var state = Verify();

            if (!state.IsValid)
                WriteAndThrow(new PackageException(state.Reason));

            WriteSuccess($"Package {Description} has been successfully verified.");

            try
            {
                if (!System.IO.Directory.Exists(Path))
                    System.IO.Directory.CreateDirectory(Path);
                CopyFilesRecursively(ArchiveName, new DirectoryInfo(Path).FullName);
            }
            catch (IOException)
            {
                WriteWarn($"{Description} data already exists. This is fine!");
            }

            WriteSuccess($"{Description} data has been installed successfully to the filesystem.");

            /// <see cref="https://stackoverflow.com/a/3822913/14894786"/>
            void CopyFilesRecursively(string sourcePath, string targetPath)
            {
                foreach (string dirPath in System.IO.Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    System.IO.Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }
                foreach (string newPath in System.IO.Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            }
        }
    }
}
