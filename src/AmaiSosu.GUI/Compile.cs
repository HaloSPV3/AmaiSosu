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
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using AmaiSosu.GUI.Properties;

namespace AmaiSosu.GUI
{
    public class Compile : INotifyPropertyChanged
    {
        private string _path;
        private List<string> _files;
        private string _destination;
        private Visibility _visibility = Visibility.Collapsed;

        public void Invoke()
        {
            Task.Run(() => {
                HXE.SFX.Compile(new HXE.SFX.Configuration
                {
                    Source = new DirectoryInfo(Environment.CurrentDirectory),
                    Target = new DirectoryInfo(Packages(Path)),
                    Executable = new FileInfo(Assembly.GetExecutingAssembly().Location)
                });
            });
        }

        public string Path
        {
            get => _path;
            set
            {
                if (value == _path) return;
                _path = value;
                OnPropertyChanged();
            }
        }

        public List<string> Files
        {
            get => _files;
            set
            {
                if (value == _files) return;
                _files = value;

            }
        }

        /// <summary>
        ///     Directory where the completed SFX assembly is saved
        /// </summary>
        public string Destination
        {
            get => _destination;
            set
            {
                if (value == _destination) return;
                _destination = value;
                OnPropertyChanged();
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

        public static string Packages(string target)
        {
            return System.IO.Path.Combine(target, "data");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
