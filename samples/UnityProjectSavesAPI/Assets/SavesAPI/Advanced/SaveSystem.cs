using System;
using System.Collections.Generic;
using System.IO;

namespace SavesAPI.Advanced
{
    /*/ 
     *
     *  WARNING: use only to inherit and create custom save systems
     * 
     *  - Members -------------------------
     *  
     *      DirectoryPath                 - The directory path the save system saves to and loads from
     *      FilesPrefix                   - The prefix of every file created with the save system
     *      Events                        - Events of the save system
     *      
     *      Save(...)                     - Saves an object to a file
     *      Load(...)                     - Loads an object from a file
     *      LoadByPath(...)               - Loads an object from a file
     *      Delete(...)                   - Deletes a saved file
     *      DeleteByPath(...)             - Deletes a saved file
     *      
     *      LoadIfExists(...)             - Loads a file only if it exits
     *      LoadIfExistsByPath(...)       - Loads a file only if it exits
     *      FileExists(...)               - Checks if there is an existing saved file with a chosen name
     *      
     *      GetAllFilePaths()             - Returns all file paths of files that have been saved by the save system
     *      LoadMultiple(...)             - Loads multiple saved files
     *      LoadAllFiles()                - Loads all files that have been saved by the save system
     *      
     *      GeneratePath(...)             - Generates a path for a file based on a given file-name
     *      ExtractName(...)              - Extracts the file name from a file path
     *  
     *  - Abstract members ---------------- (implement when inheriting) 
     *  
     *      FileType                      - The file type of the saveable files
     *      SaveMethod(...)               - Internal saving method of system
     *      LoadByPathMethod(...)         - Internal loading by path method of system
     *      
     *  - Static Methods ------------------
     *  
     *      FileDelete(...)               - Deletes a file from a path only if it exists
     *      FileExistsByPath(...)         - Checks if there is an existing saved file on a given path
     *      GetDirectoryFilePaths(...)    - Returns all file paths of files in a directory that contain a given prefix
     *      MakeSureDirectoryExists(...)  - Creates a directory in a chosen path if it does not exist
     *      GetLastModificationTime(...)  - Will return the last time a file has been modified or created
     *      WebGLFileSync()               - Calls the file sync function in WebGL apps
     *      
     *  -----------------------------------    
    /*/

    /// <summary>
    /// A base class for a save system - WARNING: use only to inherit and create custom save system
    /// </summary>
    /// <typeparam name="T">A saveable object</typeparam>
    public abstract class SaveSystem<T> where T : class, ISaveable
    {
        /// <summary>
        /// The file type of the saveable files
        /// </summary>
        public abstract string FileType { get; }

        /// <summary>
        /// The directory path the save system saves-to and loads-from
        /// </summary>
        public string DirectoryPath { get; private set; }

        /// <summary>
        /// The prefix of every file created with the save system
        /// </summary>
        public string FilesPrefix { get; private set; }

        /// <summary>
        /// Events of the save system
        /// </summary>
        public SaveSystemEvents<T> Events { get; set; }

        /// <summary>
        /// A constructor for the base save system
        /// </summary>
        /// <param name="directoryPath">The directory path the save system saves to and loads from</param>
        /// <param name="filesPrefix">The prefix of every file created with the save system</param>
        public SaveSystem(string directoryPath, string filesPrefix)
        {
            Events = new SaveSystemEvents<T>();
            DirectoryPath = directoryPath;
            FilesPrefix = filesPrefix;
            MakeSureDirectoryExists(directoryPath);
        }

        /// <summary>
        /// Saves an object to a file
        /// </summary>
        /// <param name="toSave">Object to save</param>
        public void Save(T toSave)
        {
            SaveMethod(toSave);
            Events.OnSaved(toSave);
            WebGLFileSync();
        }

        /// <summary>
        /// Loads an object from a file
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns></returns>
        public T Load(string fileName) => 
            LoadByPath(GeneratePath(fileName));

        /// <summary>
        /// Loads an object from a file
        /// </summary>
        /// <param name="filePath">The path of the file</param>
        /// <returns></returns>
        public T LoadByPath(string filePath)
        {
            var load = LoadByPathMethod(filePath);
            Events.OnLoaded(load);
            load.LastUsage = GetLastModificationTime(filePath);
            return load;
        }

        /// <summary>
        /// Deletes a saved file
        /// </summary>
        public void Delete(string fileName) =>
            DeleteByPath(GeneratePath(fileName));

        /// <summary>
        /// Deletes a saved file
        /// </summary>
        public void DeleteByPath(string filePath)
        {
            FileDelete(filePath);
            Events.OnDeleted(filePath);
        }

        /// <summary>
        /// Loads a file only if it exits
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns><see cref="null"/> if file does not exist</returns>
        public T LoadIfExists(string fileName) => 
            LoadIfExistsByPath(GeneratePath(fileName));

        /// <summary>
        /// Loads a file only if it exits
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns><see cref="null"/> if file does not exist</returns>
        public T LoadIfExistsByPath(string filePath)
        {
            if (FileExistsByPath(filePath))
                return LoadByPath(filePath);
            return null;
        }

        /// <summary>
        /// Checks if there is an existing saved file with a chosen name
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns></returns>
        public bool FileExists(string fileName) =>
            FileExistsByPath(GeneratePath(fileName));

        /// <summary>
        /// Returns all file paths of files that have been saved by the save system
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFilePaths() =>
            GetDirectoryFilePaths(DirectoryPath, FilesPrefix);

        /// <summary>
        /// Loads multiple saved files
        /// </summary>
        /// <param name="filePaths">All paths of files you want to load</param>
        public T[] LoadMultiple(string[] filePaths)
        {
            var files = new T[filePaths.Length];
            for (int i = 0; i < filePaths.Length; i++)
                files[i] = LoadByPath(filePaths[i]);
            return files;
        }

        /// <summary>
        /// Loads all files that have been saved by the save system
        /// </summary>
        public T[] LoadAllFiles() => LoadMultiple(GetAllFilePaths().ToArray());

        /// <summary>
        /// Generates a path for a file based on a given file-name
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns></returns>
        public string GeneratePath(string fileName) =>
            PathGenerator.GeneratePathFile(DirectoryPath, FilesPrefix, fileName, FileType);

        /// <inheritdoc cref="PathGenerator.ExtractFileNameFromPath(string, string, string, string)" />
        public string ExtractName(string filePath) =>
            PathGenerator.ExtractFileNameFromPath(DirectoryPath, FilesPrefix, FileType, filePath);

        protected abstract void SaveMethod(T toSave);
        protected abstract T LoadByPathMethod(string filePath);

        /// <summary>
        /// Deletes a file from a path only if it exists
        /// </summary>
        /// <param name="filePath">Path of file</param>
        public static void FileDelete(string filePath)
        {
            if (FileExistsByPath(filePath))
            {
                File.Delete(filePath);
                WebGLFileSync();
            }
        }

        /// <summary>
        /// Checks if there is an existing saved file on a given path
        /// </summary>
        /// <param name="filePath">Path to check</param>
        /// <returns></returns>
        public static bool FileExistsByPath(string filePath) => File.Exists(filePath);

        /// <summary>
        /// Returns all file paths of files that have been saved by the save system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folderPath">Files directory</param>
        /// <param name="filePrefix">Prefix of saveable files</param>
        public static List<string> GetDirectoryFilePaths(string folderPath, string filePrefix)
        {
            MakeSureDirectoryExists(folderPath);

            var files = new List<string>();
            var filePaths = Directory.GetFiles(folderPath);

            for (int i = 0; i < filePaths.Length; i++)
                if (filePaths[i].Contains(filePrefix))
                    files.Add(filePaths[i]);

            return files;
        }

        /// <summary>
        /// Creates a directory in a chosen path if it does not exist
        /// </summary>
        /// <param name="dirPath">The path of the directory</param>
        public static void MakeSureDirectoryExists(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                WebGLFileSync();
            }
        }

        /// <summary>
        /// Will return the last time a file has been modified or created
        /// </summary>
        /// <param name="path">Path of file</param>
        /// <returns></returns>
        private static DateTime GetLastModificationTime(string path) => File.GetLastWriteTime(path);

        /// <summary>
        /// Calls the file sync function in WebGL apps
        /// </summary>
        private static void WebGLFileSync()
        {
#if UNITY_WEBGL
        Application.ExternalEval("_JS_FileSystem_Sync();");
#endif
        }
    }
}
