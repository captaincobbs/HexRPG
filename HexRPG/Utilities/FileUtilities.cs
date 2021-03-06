using System;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace HexRPG.Utilities
{
    public class FileUtilities
    {
        /// <summary>
        /// Determines the format in which user files will be exported as
        /// </summary>
        public enum SaveType { JSON = 0, Binary = 1, XML = 2}

        /// <summary>
        /// Tracks the last location of any loaded file.
        /// </summary>
        private static string previousLoadDirectory = null;

        /// <summary>
        /// Returns the <see cref="FileStream"/> associated with the provided file. Displays an
        /// error to the user or throws an exception, based on <paramref name="silentMode"/>.
        /// </summary>
        /// <param name="path">The full path to the file.</param>
        /// <param name="silentMode">
        /// If true, exceptions are re-raised after being logged. Otherwise, the user is presented
        /// with an error and the exception is caught.
        /// </param>
        public static FileStream OpenFile(string path, FileMode mode, bool silentMode = false)
        {
            try
            {
                return new FileStream(path, mode);
            }
            catch (Exception ex)
            {
                string fileName = Path.GetFileName(path);
                string message = $"The file \"{fileName}\" could not be loaded.";
                LogUtilities.Log(ex, message);

                if (silentMode)
                {
                    throw;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the deserialized output of the provided path. Logs and returns default(T) for failures. Requires a SaveType to determine what filetype is being loaded.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="saveType">Type of </param>
        /// <param name="path">The path of the file being deserialized.</param>
        /// <param name="throwErrors"
        /// If true, exceptions are rethrown. Otherwise, an error message will explain each error.
        /// Errors are not logged if exceptions are rethrown.></param>
        /// <returns></returns>
        public static T LoadFile<T>(SaveType saveType, string path, bool throwErrors = false)
        {
            if ((int)saveType == 1)
            {
                return LoadJson<T>(path, throwErrors);
            }
            else
            {
                return LoadBinary<T>(path, throwErrors);
            }
        }

        /// <summary>
        /// Returns the deserialized output of the provided path. Logs and returns default(T) for
        /// failures.
        /// </summary>
        /// <typeparam name="T">Binary deserializes to this type.</typeparam>
        /// <param name="path">The path of the file being deserialized.</param>
        /// <param name="throwErrors">
        /// If true, exceptions are rethrown. Otherwise, an error message will explain each error.
        /// Errors are not logged if exceptions are rethrown.
        /// </param>
        public static T LoadBinary<T>(string path, bool throwErrors = false)
        {
            if (!File.Exists(path))
            {
                string exception = $"The file could not be loaded since \"{path}\" is not a valid directory.";

                if (throwErrors)
                {
                    throw new FileNotFoundException(exception);
                }

                LogUtilities.Log(exception);
                return default;
            }

            try
            {
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    var binaryFormatter = new BinaryFormatter();
                    return (T)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                if (throwErrors)
                {
                    throw;
                }

                string fileName = Path.GetFileName(path);
                string message = $"The file \"{fileName}\" could not be loaded.";

                LogUtilities.Log(ex, message);
            }

            return default;
        }

        /// <summary>
        /// Returns the deserialized output of the provided path. Logs and returns default(T) for
        /// failures.
        /// </summary>
        /// <typeparam name="T">JSON deserializes to this type.</typeparam>
        /// <param name="path">The path of the file being deserialized.</param>
        /// <param name="throwErrors">
        /// If true, exceptions are rethrown. Otherwise, an error message will explain each error.
        /// Errors are not logged if exceptions are rethrown.
        /// </param>
        public static T LoadJson<T>(string path, bool throwErrors = false)
        {
            if (!File.Exists(path))
            {
                string exception = $"The file could not be loaded since \"{path}\" is not a valid directory.";

                if (throwErrors)
                {
                    throw new FileNotFoundException(exception);
                }

                LogUtilities.Log(exception);
                return default;
            }

            try
            {
                string text = File.ReadAllText(path);
                T jsonObject = JsonConvert.DeserializeObject<T>(text);

                return jsonObject;
            }
            catch (Exception ex)
            {
                if (throwErrors)
                {
                    throw;
                }

                string fileName = Path.GetFileName(path);
                string message = $"The file \"{fileName}\" could not be loaded.";

                LogUtilities.Log(ex, message);
            }

            return default;
        }

        /// <summary>
        /// Serializes an object and exports it as a file, what the object is exported to depends on the SaveType. Returns file path of written object.
        /// </summary>
        /// <param name="saveType">What the object will be exported as</param>
        /// <param name="obj">Object to be serialized/exported</param>
        /// <param name="path">Path to which the object will be written</param>
        /// <param name="throwErrors">
        /// If true, exceptions are rethrown. Otherwise, a generic error message will be shown.
        /// Errors are not logged if exceptions are rethrown.</param>
        /// <exception cref="FileNotFoundException">
        /// If throwErrors is true, this is thrown for an invalid path.
        /// </exception>
        /// <returns></returns>
        public static string SaveFile(SaveType saveType, object obj, string path, bool throwErrors = false)
        {
            if ((int)saveType == 1)
            {
                return SaveFileJson(obj, path, throwErrors);
            }
            else
            {
                return SaveFileBinary(obj, path, throwErrors);
            }
        }

        /// <summary>
        /// Uses Binary Writer to write an object to the provided path. Logs failures. Returns the file path on
        /// success, null otherwise.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// If throwErrors is true, this is thrown for an invalid path.
        /// </exception>
        /// <param name="obj">An object to be written into Binary.</param>
        /// <param name="path">The path the binary writer writes to.</param>
        /// <param name="throwErrors">
        /// If true, exceptions are rethrown. Otherwise, a generic error message will be shown.
        /// Errors are not logged if exceptions are rethrown.
        /// </param>
        public static string SaveFileBinary(object obj, string path, bool throwErrors = false)
        {
            string directory = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            FileStream fileStream = new FileStream(path, FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                formatter.Serialize(fileStream, obj);

                return path;
            }
            catch (Exception ex)
            {
                if (throwErrors)
                {
                    throw;
                }

                string message = $"The file \"{fileName}\" could not be saved.";

                LogUtilities.Log(ex, message);
            }
            finally
            {
                fileStream.Close();
            }

            return null;
        }

        /// <summary>
        /// Serializes an object to the provided path. Logs failures. Returns the file path on
        /// success, null otherwise.
        /// </summary>
        /// <exception cref="FileNotFoundException">
        /// If throwErrors is true, this is thrown for an invalid path.
        /// </exception>
        /// <param name="obj">An object to be serialized in JSON.</param>
        /// <param name="path">The path to save to.</param>
        /// <param name="throwErrors">
        /// If true, exceptions are rethrown. Otherwise, a generic error message will be shown.
        /// Errors are not logged if exceptions are rethrown.
        /// </param>
        public static string SaveFileJson(object obj, string path, bool throwErrors = false)
        {
            string directory = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string text = JsonConvert.SerializeObject(obj);
                File.WriteAllText(path, text);

                return path;
            }
            catch (Exception ex)
            {
                if (throwErrors)
                {
                    throw;
                }

                string message = $"The file \"{fileName}\" could not be saved.";

                LogUtilities.Log(ex, message);
            }

            return null;
        }

        /// <summary>
        /// Prompts the user to select a file and append to that file. Returns the file path on
        /// success, null otherwise.
        /// </summary>
        /// <param name="logs">The logs to be written to file.</param>
        /// <param name="logPath">Overrides the location to log to, if provided.</param>
        public static void SaveLog(string logs, string logPath = null)
        {
            string path = "";

            try
            {
                path = logPath ?? Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
                File.AppendAllText(path, logs);
            }
            catch (Exception ex)
            {
                string message = $"The logs could not be saved at \"{path}\"";
                LogUtilities.Log(ex, message);
            }
        }

        /// <summary>
        /// Prompts the user to select a file and returns the deserialized output of that file.
        /// Returns default(T) if the user did not select a file. Logs failures.
        /// </summary>
        /// <typeparam name="T">JSON deserializes to this type.</typeparam>
        /// <param name="title">The title to display in the open file dialog.</param>
        /// <param name="throwErrors">
        /// If true, exceptions are rethrown. Otherwise, an error message will explain each error.
        /// Errors are not logged if exceptions are rethrown.
        /// </param>
        public static T PromptLoadJson<T>(string title, bool throwErrors = false)
        {
            // TODO - Bug 1
            OpenFileDialog dialog = new OpenFileDialog
            {
                CheckPathExists = true,
                Filter = "JSON|*.json",
                InitialDirectory = previousLoadDirectory,
                Title = title
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    string text = File.ReadAllText(dialog.FileName);
                    T jsonObject = JsonConvert.DeserializeObject<T>(text);
                    previousLoadDirectory = Path.GetDirectoryName(dialog.FileName);

                    return jsonObject;
                }
                catch (Exception ex)
                {
                    if (throwErrors)
                    {
                        throw;
                    }

                    string str = "The file could not be loaded.";

                    if (ex is System.IO.DirectoryNotFoundException)
                    {
                        str += $" The chosen directory could not be found: {dialog.FileName}";
                    }
                    else if (ex is System.IO.FileNotFoundException)
                    {
                        str += $" The file no longer exists or can't be located at: {dialog.FileName}";
                    }
                    else if (ex is System.IO.IOException)
                    {
                        // No additional information.
                    }
                    else if (ex is System.IO.PathTooLongException)
                    {
                        str += $" The full path including the filename is longer than permitted: {dialog.FileName}";
                    }
                    else if (ex is UnauthorizedAccessException)
                    {
                        str += $" Either the file is in use by another program, or you don't have permission to read from this location: {dialog.FileName}";
                    }
                    else if (ex is NotSupportedException)
                    {
                        str += $" The file could not be read or reading is unsupported for this file type: {dialog.FileName}";
                    }
                    else if (ex is System.Security.SecurityException)
                    {
                        str += $" You might not have permission to read from this location: {dialog.FileName}";
                    }

                    LogUtilities.Log(ex, str.ToString());
                    MessageBox.Show(str.ToString());
                }
            }

            return default;
        }
    }
}
