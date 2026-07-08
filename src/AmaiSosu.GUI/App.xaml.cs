/**
 * Copyright (C) 2018-2019 Emilian Roman
 * Copyright (C) 2021 Noah Sherwin
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

using System.IO;
using System.Windows;
using ASStartup = AmaiSosu.Startup;

namespace AmaiSosu.GUI
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal struct Arg
        {
            public const string Auto = "--auto";
            public const string Compile = "--compile";
            public const string Help = "--help";
            public const string Path = "--path=";
            public const string Memo = "--memo=";
        }

        private void AppStart(object sender, StartupEventArgs args)
        {
            var Args = args.Args;

            /** Set Startup Settings */
            if (Args.Length != 0)
            {
                foreach (var arg in Args)
                {
                    // if arg is enclosed with double-quotes, take the inner string
                    string _arg = arg[0] == '"' && arg[arg.Length - 1] == '"'
                              ? arg.Substring(1, arg.Length - 2)
                              : arg;
                    switch (_arg)
                    {
                        case var text when text == Arg.Auto:
                            {
                                ASStartup.Auto = true;
                                break;
                            }
                        case var text when text == Arg.Compile:
                            {
                                ASStartup.Compile = true;
                                break;
                            }
                        case var text when text == Arg.Help:
                            {
                                ASStartup.Help = true;
                                break;
                            }
                        // Used by either Compile or Install; If you need more path validation, don't do it here!
                        case var text when text.StartsWith(Arg.Path):
                            {
                                /* Remove remaining double-quotes; then, take the argument's value (i.e. everything after "--path=") */
                                text = text.Replace("\"", string.Empty)[Arg.Path.Length..];
                                // Communicating the error to the user would be nice
                                try { text = Path.GetFullPath(text); }
                                catch { }

                                ASStartup.Path = text;
                                break;
                            }
                        default: break;
                    }
                }
            }
            new MainWindow().Show();
        }
    }
}
