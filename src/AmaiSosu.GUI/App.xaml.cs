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
using System.Windows;
using static AmaiSosu.Startup;

namespace AmaiSosu.GUI
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStart(object sender, StartupEventArgs e)
        {
            /** Set Startup Settings */
            if (e.Args.Length != 0)
            {
                foreach (var arg in e.Args)
                {
                    switch(arg)
                    {
                        case "--auto":
                            Auto = true;
                            break;
                        case "--compile":
                            AmaiSosu.Startup.Compile = true;
                            break;
                        case "--help":
                            AmaiSosu.Startup.Help = true;
                            break;
                        case "--path":
                            int index = Array.IndexOf(e.Args, arg);
                            int next = index + 1;
                            if (e.Args.Length < next)
                                break;
                            if (e.Args[next].StartsWith("--"))
                                break;
                            if (!System.IO.Path.IsPathRooted(e.Args[next]))
                                break;
                            AmaiSosu.Startup.Path = e.Args[next];
                            break;
                        default:
                            break;
                    }
                }
            }

            new MainWindow().Show();
        }
    }
}
