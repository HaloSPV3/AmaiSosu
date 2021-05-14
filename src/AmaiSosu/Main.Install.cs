/**
 * Copyright (C) 2018-2019 Emilian Roman
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AmaiSosu.Common.IO;
using AmaiSosu.Installation;
using Paths = AmaiSosu.Common.Paths;
using static AmaiSosu.Resources.FileNames;

namespace AmaiSosu
{
    public sealed partial class Main
    {
        /// <summary>
        ///     Invokes the installation procedure.
        /// </summary>
        public void Install()
        {
            var backupDir = Path.Combine(_path, AmaiSosuBackup + '.' + Guid.NewGuid());

            CommitBackups(backupDir);
            new InstallerFactory(_path).GetInstaller().Install();
            FinishInstall(backupDir);
        }

        /// <summary>
        ///     Conducts the OpenSauce & HAC2 data backup routines.
        /// </summary>
        /// <param name="backupDir">
        ///     Backup directory to use for backing up OpenSauce & HAC2 data.
        /// </param>
        private void CommitBackups(string backupDir)
        {
            Directory.CreateDirectory(backupDir);

            new List<Move>
            {
                MoveFactory.Get(MoveFactory.Type.BackupOsFiles, _path, backupDir),
                MoveFactory.Get(MoveFactory.Type.BackupOsDirectories, _path, backupDir),
                MoveFactory.Get(MoveFactory.Type.BackupHac2Files, _path, backupDir)
            }.ForEach(move => move.Commit());

            Directory.Move(Paths.KStudios, Path.Combine(backupDir, OpenSauceDeveloper));
        }

        /// <summary>
        ///     Conducts optional installation finalisation routines.
        ///     - Restore the HCE shaders.
        ///     - Move the OpenSauce IDE.
        ///     - Empty backup directory cleanup.
        /// </summary>
        /// <param name="backupDir"></param>
        private void FinishInstall(string backupDir)
        {
            // restore backed up HCE shaders
            MoveFactory.Get(MoveFactory.Type.RestoreHceShaders, _path, backupDir)
                .Commit();

            // copy OS IDE to Install path
            var source =
                Path.Combine(Paths.KStudios, OpenSauceDirectory, OpenSauceIDE);

            var target = Path.Combine(_path, OpenSauceIDE);

            Copy.All(source, target);

            // cleans up empty backup directory
            if (!Directory.EnumerateFileSystemEntries(backupDir).Any())
                Directory.Delete(backupDir);
        }
    }
}
