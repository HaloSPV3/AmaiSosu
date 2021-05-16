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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using AmaiSosu.GUI.Properties;
using AmaiSosu.GUI.Resources;
using static System.Environment;

namespace AmaiSosu.GUI
{
    public class Compile : INotifyPropertyChanged
    {
        private bool _canCompile;
        private string _compileText = "Locate the files to package.";

        private string _source = string.IsNullOrWhiteSpace(Startup.Path) ?
            CurrentDirectory :
            Startup.Path;

        private Visibility _visibility = Visibility.Collapsed;

        /// <summary>
        ///     Gets or sets a value indicating whether AmaiSosu can compile a release.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance can compile; otherwise, <c>false</c>.
        /// </value>
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
        ///     Gets or sets the compile text message.
        /// </summary>
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
        ///     Gets or sets the path containing OpenSauce binaries.
        /// </summary>
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
        /// TODO instead of a bool, return a SUCCESS or FAILURE enum
        public bool Invoke()
        {
            try
            {
                CompileText = "Compiling...";
                new AmaiSosu.Main(Source).Compile();
                CompileText = Messages.CompileSuccess;
                return true;
            }
            catch (Exception e)
            {
                CompileText = e.Message;
                return false;
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
