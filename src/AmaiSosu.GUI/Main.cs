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
using System.Windows;
using System.Threading.Tasks;

namespace AmaiSosu.GUI
{
    /// <summary>
    ///     Main AmaiSosu model.
    /// </summary>
    public sealed partial class Main : INotifyPropertyChanged
    {
        /// <summary>
        ///     Installation path.
        ///     Path is expected to contain the HCE executable.
        /// </summary>
        private string _path;

        public MainCompile MCompile { get; set; } = new MainCompile();
        public MainInstall MInstall { get; set; } = new MainInstall();

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
        ///     Installation path.
        /// </summary>
        public string Path
        {
            get => _path;
            set
            {
                if (value == _path) return;
                _path = value;
                OnPropertyChanged();
                OnPathChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Initialise the HCE path detection attempt.
        /// </summary>
        public void Initialise()
        {
            switch (Context.Infer())
            {
                case Context.Type.Compile:
                    MCompile.Visibility = Visibility.Visible;
                    Task.Run(() => {
                        HXE.SFX.Compile(new HXE.SFX.Configuration
                        {
                            Source = new DirectoryInfo(Environment.CurrentDirectory),
                            Target = new DirectoryInfo(Packages(Path)),
                            Executable = new FileInfo(Assembly.GetExecutingAssembly().Location)
                        });
                    });
                    break;
                case Context.Type.Install:
                    MInstall.Visibility = Visibility.Visible;
                    try
                    {
                        Path = !string.IsNullOrWhiteSpace(Startup.Path) ?
                           Startup.Path :
                           System.IO.Path.GetDirectoryName(Loader.Detect());

                        OnPathChanged();

                        if (Startup.Auto)
                            MInstall.Install();
                    }
                    catch (Exception)
                    {
                        MInstall.InstallText = Messages.BrowseHce;
                    }
                    break;
                case Context.Type.Help:
                    // TODO: Create Help control
                    // HelpMode.Visibility = Visibility.Visible;
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static string Packages(string target)
        {
            return System.IO.Path.Combine(target, "data");
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
