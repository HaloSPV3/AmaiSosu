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

namespace AmaiSosu.Detection
{
    /// <summary>
    ///     Static API for the Atarashii Loader Module.
    /// </summary>
    public static class Loader
    {
        /// <summary>
        ///     Attempts to detect the path of the HCE executable on the file system.
        /// </summary>
        /// <returns>
        ///     Path to the HCE executable, assuming its installation is legal.
        /// </returns>
        public static string Detect()
        {
            return ExecutableFactory.Get(ExecutableFactory.Type.Detect).Path;
        }
    }
}