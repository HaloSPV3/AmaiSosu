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

using AmaiSosu.Detection;

namespace AmaiSosu
{
    public static class Startup
    {
        /// <summary>
        /// Set by the presence of the --auto startup argument.
        /// Either the Compile or Install procedure will Invoke()
        /// with default parameters.
        /// </summary>
        public static bool Auto = false;

        /// <summary>
        /// Set by the presence of the --compile startup argument.
        /// Indicates to AmaiSosu that it should load the
        /// Release Compilation procedure.
        /// </summary>
        public static bool Compile = false;

        /// <summary>
        /// Set by the presence of the --help startup argument.
        /// Indicates to AmaiSosu that it should load the
        /// Help procedure to display the startup arguments to
        /// the user.
        /// </summary>
        public static bool Help = false;

        /// <summary>
        /// String passed via the --path startup argument.
        /// </summary>
        public static string Path = System.IO.Path.GetDirectoryName(Loader.Detect());
    }
}
