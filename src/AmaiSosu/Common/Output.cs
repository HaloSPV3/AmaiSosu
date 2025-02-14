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

namespace AmaiSosu.Common
{
    /// <summary>
    ///     Abstract representing an object that outputs inbound messages.
    /// </summary>
    public abstract class Output
    {
        /// <summary>
        ///     Various types for a log message.
        /// </summary>
        public enum Type
        {
            /// <summary>
            ///     Represents a successful step.
            /// </summary>
            Success,

            /// <summary>
            ///     Represents an informative message.
            /// </summary>
            Info,

            /// <summary>
            ///     Represents a non-fatal warning message.
            /// </summary>
            Warn,

            /// <summary>
            ///     Represents an error message.
            /// </summary>
            Error,

            /// <summary>
            ///     Represents a data dump.
            /// </summary>
            Dump
        }

        /// <summary>
        ///     Logs an inbound message.
        /// </summary>
        /// <param name="type">
        ///     Type of log. <see cref="Type" />
        /// </param>
        /// <param name="subject">
        ///     Subject of the message.
        ///     <example>
        ///         Calling assembly.
        ///     </example>
        /// </param>
        /// <param name="message">
        ///     Actual contents of the log.
        ///     <example>
        ///         Successfully invoked the loading mechanism.
        ///     </example>
        /// </param>
        public abstract void Write(Type type, string subject, string message);

        /// <summary>
        ///     Outputs a Type.Pass message.
        /// </summary>
        /// <param name="subject">
        ///     Subject of the message.
        /// </param>
        /// <param name="message">
        ///     Message to write.
        /// </param>
        public void WriteSuccess(string subject, string message)
        {
            Write(Type.Success, subject, message);
        }

        /// <summary>
        ///     Outputs a Type.Info message.
        /// </summary>
        /// <param name="subject">
        ///     Subject of the message.
        /// </param>
        /// <param name="message">
        ///     Message to write.
        /// </param>
        public void WriteInfo(string subject, string message)
        {
            Write(Type.Info, subject, message);
        }

        /// <summary>
        ///     Outputs a Type.Warn message.
        /// </summary>
        /// <param name="subject">
        ///     Subject of the message.
        /// </param>
        /// <param name="message">
        ///     Message to write.
        /// </param>
        public void WriteWarn(string subject, string message)
        {
            Write(Type.Warn, subject, message);
        }

        /// <summary>
        ///     Outputs a Type.Error message.
        /// </summary>
        /// <param name="subject">
        ///     Subject of the message.
        /// </param>
        /// <param name="message">
        ///     Message to write.
        /// </param>
        public void WriteError(string subject, string message)
        {
            Write(Type.Error, subject, message);
        }

        /// <summary>
        ///     Outputs a Type.Dump message.
        /// </summary>
        /// <param name="subject">
        ///     Subject of the message.
        /// </param>
        /// <param name="message">
        ///     Message to write.
        /// </param>
        public void WriteDump(string subject, string message)
        {
            Write(Type.Dump, subject, message);
        }
    }
}