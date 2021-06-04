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
using System.ComponentModel;
using System.Security.Principal;
using System.Windows;
using Tasks = Intern.Tasks;

namespace AmaiSosu.GUI
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void AppStart(object sender, StartupEventArgs args)
        {
            var Args = args.Args;

            /** Set Startup Settings */
            if (Args.Length != 0)
            {
                foreach (var arg in Args)
                {
                    if (!AmaiSosu.Startup.Auto && arg.ToLower().Contains("--auto"))
                        AmaiSosu.Startup.Auto = true;
                    else if (!AmaiSosu.Startup.Compile && arg.ToLower().Contains("--compile"))
                        AmaiSosu.Startup.Compile = true;
                    else if (!AmaiSosu.Startup.Help && arg.ToLower().Contains("--help"))
                        AmaiSosu.Startup.Help = true;
                    else if (arg.Contains("--path="))
                    {
                        var path = arg.Replace("--path=", string.Empty);
                        path = path.Replace("\"", string.Empty);
                        try
                        {
                            if (System.IO.Path.IsPathRooted(path))
                                AmaiSosu.Startup.Path = path;
                        }
                        catch (ArgumentException e)
                        {
                            throw new ArgumentException($"The path supplied to --path was invalid: {path}", e);
                        }
                    }
                    /// Tasks to execute with different Windows user/group permissions.
                    else if (arg.Contains("--special-task="))
                    {
                        // example 1: --special-task=FileSystemDelete{""}
                        // example 2: --special-task=FileSystemModifyPermissions{""}
                        string task = arg.Replace("--special-task=", string.Empty); // remove argument's prefix
                        task = task.Replace("\"", string.Empty); // remove quotation marks
                        switch (task)
                        {
                            case string task1 when task1.StartsWith("FileSystemDelete{") && task1.EndsWith("}"):
                                var path = task1.Replace("FileSystemDelete{", string.Empty).Replace("}", string.Empty); // trim matched characters
                                Tasks.DoTask(Tasks.Type.FileSystemDelete, path);
                                break;

                            case string task2 when task2.StartsWith("FileSystemModifyPermissions") && task2.EndsWith(""):
                                break;

                            default:
                                throw new ArgumentOutOfRangeException("The specified Task is unrecognized.");
                        }
                    }
                }
            }
            new MainWindow().Show();
        }
    }
}
