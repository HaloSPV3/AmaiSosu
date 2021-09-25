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
using Intern;
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
                    switch (arg)
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
                        case var text when text.StartsWith(Arg.Path):
                            {
                                try
                                {
                        catch (ArgumentException e)
                        {
                            throw new ArgumentException($"The path supplied to --path was invalid: {path}", e);
                        }
                    }

                                    ASStartup.Path = dir.FullName;
                                }
                                catch (Exception)
                                {
                                }

                                break;
                            }
                        /// Tasks to execute with different Windows user/group permissions.
                        case var text when text.StartsWith(Arg.Memo):
                            {
                                // example: --intern-memo="%temp%\\123abc.tmp"
                                string memo = text.Replace(Arg.Memo, string.Empty); // remove argument's prefix
                                memo = memo.Replace(@"\", string.Empty); // remove quotation marks

                                if (new FileInfo(memo).Exists)
                                {
                                    var status = Memo.Read(memo);
                                    if (status.State == Status.Type.Failed)
                                    {
                                        var msg = $"The Intern failed to complete their task(s). Reason: {status.Message}{NewLine}{status.exception.Message}";
                                        MessageBox.Show(msg, "Error: Task Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                                    }
                                    /// TODO: Memo reading (and writing, but that goes somewhere else!)
                                }
                                else
                                {
                                    var msg = $"The Intern's Memo could not be found at the path \"{new Uri(memo)}\"";
                                    MessageBox.Show(msg, "Error: File Not Found", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
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
