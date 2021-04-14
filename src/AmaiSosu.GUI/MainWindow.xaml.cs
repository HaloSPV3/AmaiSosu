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
using System.Diagnostics;
using System.Windows;
using MahApps.Metro.Controls;

namespace AmaiSosu.GUI
{
    public partial class MainWindow : MetroWindow
    {
        private Main _main;
        private Main.MainCompile _compile;
        private Main.MainHelp    _help;
        private Main.MainInstall _install;


        public MainWindow()
        {
            InitializeComponent();
            _main = (Main) DataContext;
            _main.Initialise();

            switch(_main.Mode)
            {
                case Context.Type.Compile:
                    _compile = new Main.MainCompile();
                    break;
                case Context.Type.Help:
                    _help = new Main.MainHelp();
                    break;
                case Context.Type.Install:
                    _install = new Main.MainInstall();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void About(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/HaloSPV3/AmaiSosu");
        }

        private void Releases(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/HaloSPV3/AmaiSosu/releases");
        }

        private void Version(object sender, RoutedEventArgs e)
        {
            Process.Start($"https://github.com/HaloSPV3/AmaiSosu/releases/{_main.Version}");
        }
  }
}
