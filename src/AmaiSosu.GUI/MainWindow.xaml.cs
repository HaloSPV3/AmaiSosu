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

using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls;
using Microsoft.Win32;

namespace AmaiSosu.GUI
{
    public partial class MainWindow : MetroWindow
    {
        private readonly Main _main;

        public MainWindow()
        {
            InitializeComponent();
            _main = (Main) DataContext;
            _main.Initialise();

            var UCCompile = new UserControlCompile{ Visibility = Visibility.Collapsed };
            var UCHelp = new UserControlHelp{ Visibility = Visibility.Collapsed };
            var UCInstall = new UserControlInstall{ Visibility = Visibility.Collapsed };
        }

        private async void Install(object sender, RoutedEventArgs e)
        {
            InstallButton.IsEnabled = false;

            await Task.Run(() => _main.Install());

            InstallButton.IsEnabled = true;
        }

        private void Browse(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "HCE Executable|haloce.exe"
            };

            if (openFileDialog.ShowDialog() == true)
                _main.Path = Path.GetDirectoryName(openFileDialog.FileName);
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
