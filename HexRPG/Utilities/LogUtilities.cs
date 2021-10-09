using System;
using System.Collections.Generic;
using System.Text;

namespace HexRPG.Utilities
{
    public static class LogUtilities
    {
        private static readonly List<string> log;

        static LogUtilities()
        {
            log = new List<string>();
        }

        /// <summary>
        /// Includes a date-prefixed message in the log
        /// </summary>
        /// <param name="message">The message to be logged and dated</param>
        public static void Log(string message, string type = "Message")
        {
            log.Add(PrepareMessage(message, type));

            if (log.Count == 100)
            {
                Flush();
            }
        }

        /// <summary>
        /// Generates date-prefixed message with the exception details in the log, then rethrows.
        /// </summary>
        /// <param name="ex">The exception to generate logging for.</param>
        /// <param name="startMessage">Special message to prefix the exception</param>
        public static void Log(Exception ex, string startMessage = null)
        {
            string message = "Error:" + $"{startMessage} " ?? ""
                + $"Log caught exception: {ex.Message},"
                + $"with inner exception: {ex.InnerException?.Message ?? "null"},"
                + $"with stack trace: {ex.StackTrace}";
        }

        /// <summary>
        /// If an exception occurs while executing the action, generates a date-prefixed message with exception detailsin in the log, then rethrows.
        /// </summary>
        /// <param name="action">The action to execute with logging.</param>
        public static void Log(Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception ex)
            {
                Log(ex);
                throw;
            }
        }

        public static void Flush()
        {
            // Nothing to flush
            if (log?.Count == 0)
            {
                return;
            }

            // Append all strings and flush.
            string logAsString = "";
            log.ForEach((str) => logAsString += str);

            try
            {
                FileUtilities.SaveLog(logAsString);
            }
            catch (Exception)
            {
                // The log has failed to save, it isn't important enough to alert the user
            }

            log.Clear();
        }

        /// <summary>
        /// Returns a date-prefixed message (local timezone).
        /// </summary>
        /// <param name="message">The string to be logged.</param>
        private static string PrepareMessage(string message, string type)
        {
            return $"{DateTime.Now:M-dd-yyyy hh:mm:ss.fff}, {type} - {message}" + Environment.NewLine;
        }
    }
}
