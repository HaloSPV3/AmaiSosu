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

namespace AmaiSosu
{
    /// <summary>
    ///   Module for determining the scenario of the app environment.
    /// </summary>
    public static class Context
    {
        /// <summary>
        ///   Possible environment states.
        /// </summary>
        public enum Type
        {
            Compile, /* The executing assembly is NOT a self-extracting installer. */
            Install, /* The executing assembly is a self-extracting installer.  */
            Help     /* The app was started with the --help argument. */
        }

        /// <summary>
        ///   Determines the environment context.
        /// </summary>
        /// <returns>
        ///   <see cref="Type" />
        /// </returns>
        public static Type Infer()
        {
            if (Startup.Compile)
                return Type.Compile;
            else if (Startup.Help)
                return Type.Help;
            else return Type.Install;
        }
    }
}
