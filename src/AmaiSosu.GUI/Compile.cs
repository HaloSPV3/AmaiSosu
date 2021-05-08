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
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using AmaiSosu.GUI.Properties;
using System.Diagnostics;
using static System.Environment;

namespace AmaiSosu.GUI
{
    public class Compile : INotifyPropertyChanged
    {
        private bool _canCompile;
        private List<string> _files;
        private string _compileText = "Locate the files to package.";
        private string _source = string.IsNullOrWhiteSpace(Startup.Path) ?
            CurrentDirectory :
            Startup.Path;
        private Visibility _visibility = Visibility.Collapsed;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can compile.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can compile; otherwise, <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for CanCompile
        public bool CanCompile
        {
            get => _canCompile;
            set
            {
                if (value == _canCompile) return;
                _canCompile = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>
        /// The files.
        /// </value>
        /// TODO Edit XML Comment Template for Files
        public List<string> Files
        {
            get => _files;
            set
            {
                if (value.SequenceEqual(_files)) return;
                _files = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the compile text.
        /// </summary>
        /// <value>
        /// The compile text.
        /// </value>
        /// TODO Edit XML Comment Template for CompileText
        public string CompileText
        {
            get => _compileText;
            set
            {
                if (value == _compileText) return;
                _compileText = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        /// TODO Edit XML Comment Template for Path
        public string Source
        {
            get => _source;
            set
            {
                if (value == _source) return;
                _source = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Invokes HXE's SFX Compiler
        /// </summary>
        public void Invoke()
        {
            /// We need items from two paths:
            /// "%ProgramData%\\Kornner Studios"
            /// and a directory containing OpenSauce's compiled executable/library assemblies (.exe, .dll).
            FileInfo exeFileInfo = new FileInfo(Assembly.GetEntryAssembly()?.Location
                                   ?? throw new InvalidOperationException());
            DirectoryInfo sPath = new DirectoryInfo(Source);
            DirectoryInfo tPath = new DirectoryInfo(Path.GetTempPath()).CreateSubdirectory("AmaiSosu.tmp");
            DirectoryInfo Gui = tPath.CreateSubdirectory(Installation.Installer.GuiPackage);
            DirectoryInfo Lib = tPath.CreateSubdirectory(Installation.Installer.LibPackage);
            string KStudios = "Kornner Studios";

            try
            {
                /** Copy libraries to temporary directory */
                CopyFilesRecursively(Source, Lib.FullName);
                /** Copy Kornner Studios to temp directory */
                CopyFilesRecursively(
                    Path.Combine(GetFolderPath(SpecialFolder.CommonApplicationData), KStudios),
                    Path.Combine(Gui.FullName, KStudios));
            }
            catch(Exception e)
            {
                CanCompile = false;
                CompileText = e.Message;
                return;
            }

            Task.Run(() => {
                CompileText = "Compiling...";
                HXE.SFX.Compile(new HXE.SFX.Configuration
                {
                    Source = tPath,
                    Target = new DirectoryInfo(CurrentDirectory),
                    Executable = exeFileInfo
                });
                var result = RenameTarget();
                Directory.Delete(tPath.FullName, true); // cleanup
                CompileText = $"Done. File is at \"{Path.GetFullPath(result)}\"";
            });

            /// <see cref="https://stackoverflow.com/a/3822913/14894786"/>
            void CopyFilesRecursively(string sourcePath, string targetPath)
            {
                foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
                }
                foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
                }
            }

            string RenameTarget()
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(exeFileInfo.FullName);
                FileInfo sfxOut = new FileInfo(Path.Combine(CurrentDirectory, exeFileInfo.Name));
                string exeName = exeFileInfo.Name;

                exeName = exeName.Replace(".GUI.exe", $"-{fvi.ProductVersion}.exe");

                var renamedTarget = Path.Combine(CurrentDirectory, exeName);

                foreach (var process in Process.GetProcessesByName(exeName))
                {
                    process.Kill();
                }
;
                File.Delete(renamedTarget); // delete existing file
                sfxOut.MoveTo(renamedTarget);
                return renamedTarget;
            }
        }

        /// <summary>
        ///     Visibility property of the Compile UserControl
        /// </summary>
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                if (value == _visibility) return;
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
