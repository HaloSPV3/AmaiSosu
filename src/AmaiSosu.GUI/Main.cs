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

        public Compile Compile { get; set; } = new Compile();
        public Install Install { get; set; } = new Install();
        public Help    Help    { get; set; } = new Help();

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

        public string Path
        {
            get => _path;
            set
            {
                if (value == _path) return;
                _path = value;
                Compile.Path = value;
                Install.Path = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Initialise the HCE path detection attempt.
        /// </summary>
        public void Initialise()
        {
            /** See MainWindow.xaml.cs */
            switch (Mode)
            {
                case Context.Type.Compile:
                    Compile.Path = Path;
                    break;
                case Context.Type.Help:
                    // TODO: Create Help control
                    break;
                case Context.Type.Install:
                    Install.Path = string.IsNullOrWhiteSpace(Path) ?
                            System.IO.Path.GetDirectoryName(Loader.Detect()) :
                            _path;
                    try
                    {
                        if (Startup.Auto)
                            Install.Invoke();
                    }
                    catch (Exception)
                    {
                        Install.InstallText = Messages.BrowseHce;
                    }
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
