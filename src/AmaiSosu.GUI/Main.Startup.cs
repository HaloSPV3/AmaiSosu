/**
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

namespace AmaiSosu.GUI
{
    public static class Startup
    {
        /// <summary>
        /// If --auto is passed to this app,
        /// it will automatically Compile a release package
        /// or Install its package to a given path
        /// depending on the Context.
        /// </summary>
        public static bool   Auto    = false;

        /// <summary>
        /// If --compile is passed to this app,
        /// it will begin the Compile procedure
        /// to create a release package.
        /// </summary>
        public static bool   Compile = false;

        /// <summary>
        /// If --help is passed to this app,
        /// it will display a list of its command
        /// line arguments.
        /// </summary>
        public static bool   Help = false;

        /// <summary>
        /// If --path is passed to this app
        /// and is followed by a path to a directory,
        /// the string will be assigned as the default path
        /// for the Install procedure.
        /// </summary>
        public static string Path  = Environment.CurrentDirectory;
    }
}
