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
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using AmaiSosu.Detection;
using AmaiSosu.GUI.Properties;
using AmaiSosu.GUI.Resources;

namespace AmaiSosu.GUI
{
    /// <summary>
    ///     Main AmaiSosu model.
    /// </summary>
    public sealed partial class Main : INotifyPropertyChanged
    {
        /// <summary>
        ///     A multipurpose path variable.
        /// </summary>
        private string _path = string.IsNullOrWhiteSpace(Startup.Path) ?
                                Environment.CurrentDirectory :
                                Startup.Path;

        public UserControlCompile UCCompile { get; set; } = new UserControlCompile();
        public UserControlHelp    UCHelp    { get; set; } = new UserControlHelp();
        public UserControlInstall UCInstall { get; set; } = new UserControlInstall();

        /// <summary>
        ///     Git version.
        /// </summary>
        public string Version
        {
            get
            {
                using (var stream = Assembly.GetExecutingAssembly()
                    .GetManifestResourceStream(FileNames.AmaiSosuVersion))
                using (var reader = new StreamReader(stream ?? throw new FileNotFoundException()))
                {
                    return reader.ReadToEnd().Trim();
                }
            }
        }

        public Context.Type Mode
        {
            get
            {
                return Context.Infer();
            }
        }

        /// <summary>
        /// Gets or sets <see cref="_path"/> and sets
        /// <see cref="Compile.Source"/> and
        /// <see cref="Install.Path"/>.
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                if (value == _path) return;
                _path = value;
                UCCompile.Compile.Source = value;
                UCInstall.Install.Path = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Initialise the HCE path detection attempt.
        /// </summary>
        public void Initialise()
        {
            switch (Mode)
            {
                case Context.Type.Compile:
                    UCCompile.Compile.Source = Path;
                    UCCompile.Compile.Visibility = Visibility.Visible;
                    break;
                case Context.Type.Help:
                    UCHelp.Help.Visibility = Visibility.Visible;
                    break;
                case Context.Type.Install:
                    UCInstall.Install.Path = string.IsNullOrWhiteSpace(Path) ?
                            System.IO.Path.GetDirectoryName(Loader.Detect()) :
                            _path;
                    if (Startup.Auto)
                    {
                        UCInstall.Install.Visibility = Visibility.Collapsed;
                        try
                        {
                            UCInstall.Install.Invoke();
                        }
                        catch (Exception)
                        {
                            UCInstall.Install.InstallText = Messages.BrowseHce;
                        }
                    }
                    else
                        UCInstall.Install.Visibility = Visibility.Visible;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
