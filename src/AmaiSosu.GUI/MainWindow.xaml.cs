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
using System.Windows;
using MahApps.Metro.Controls;

namespace AmaiSosu.GUI
{
    public partial class MainWindow : MetroWindow
    {
        private Main _main;

        public MainWindow()
        {
            InitializeComponent();
            _main = (Main) DataContext;
            _main.Initialise();

            /// Main.Compile -> MainWindow.UCCompile.DataContext (in xaml) -> UCCompile.Compile
            /// Main.Help    -> MainWindow.UCHelp.DataContext (in xaml)    -> UCHelp.Help
            /// Main.Install -> MainWindow.UCInstall.DataContext (in xaml) -> UCInstall.Install
            UCCompile.Compile = (Compile) UCCompile.DataContext;
            UCHelp.Help       = (Help)    UCHelp.DataContext;
            UCInstall.Install = (Install) UCInstall.DataContext;

            if (Startup.Auto && _main.Success)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    bool closed = false;
                    while (!closed)
                    {
                        closed = Process.GetCurrentProcess().CloseMainWindow();
                        System.Threading.Thread.Sleep(100);
                    }
                });
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
