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

using System.Windows;
using System.Linq;
using SSO = System.StringSplitOptions;

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
                foreach (var verb in e.Args)
                {
                    if (!AmaiSosu.Startup.Auto && verb.ToLower().Contains("--auto"))
                        AmaiSosu.Startup.Auto = true;
                    else if (!AmaiSosu.Startup.Compile && verb.ToLower().Contains("--compile"))
                        AmaiSosu.Startup.Compile = true;
                    else if (!AmaiSosu.Startup.Help && verb.ToLower().Contains("--help"))
                        AmaiSosu.Startup.Help = true;
                    else if (verb.ToLower().Contains("--path="))
                    {
                        var path = verb.Replace("--path=", string.Empty);
                        path = path.Replace("\"", string.Empty);
                        try
                        {
                            path = RemoveInvalidChars(path);
                            if (System.IO.Path.IsPathRooted(path))
                                AmaiSosu.Startup.Path = path;
                        }
                        catch
                        { }
                    }
                }
            }
            new MainWindow().Show();
        }

        /// <summary>
        ///     Strip illegal characters from a path string.
        /// </summary>
        /// <see cref="https://stackoverflow.com/a/23182807/14894786"/>
        private string RemoveInvalidChars(string filename)
        {
            return string.Concat(filename.Split(System.IO.Path.GetInvalidPathChars()));
        }
    }
}
