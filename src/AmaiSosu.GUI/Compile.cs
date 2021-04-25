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

namespace AmaiSosu.GUI
{
    public class Compile : INotifyPropertyChanged
    {
        private bool _canCompile;
        private List<string> _files;
        private string _compileText = "Locate the files to package.";
        private string _source;
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
            Task.Run(() => {
                HXE.SFX.Compile(new HXE.SFX.Configuration
                {
                    Source = new DirectoryInfo(Source),
                    Target = new DirectoryInfo(Environment.CurrentDirectory),
                    Executable = new FileInfo(Assembly.GetExecutingAssembly().Location)
                });
            });
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
