using System;
using System.Collections.Generic;

namespace SavesAPI.Advanced
{
    /*/ 
     *
     *  WARNING: use only to inherit and create custom save system
     * 
     *  - Members --------------
     *  
     *      DirectoryPath            - The directory path the save system saves to and loads from
     *      FilesPrefix              - The prefix of every file created with the save system
     *      Events                   - Events of the save system
     *      Save(...)                - Saves an object to a file
     *      LoadByName(...)          - Loads an object from a file
     *      LoadByPath(...)          - Loads an object from a file
     *      LoadDirectory()          - Loads all saved files and returns the objects they store
     *      Delete(...)              - Deletes a saved file
     *      LoadIfExists(...)        - Loads a file only if it exits
     *      FileExists(...)          - Checks if there is an existing saved file with a chosen name
     *  
     *  - Abstract members ----- (implement when inheriting) 
     *  
     *      FileType                 - The file type of the saveable files
     *      SaveMethod(...)          - Internal saving method
     *      LoadByNameMethod(...)    - Internal loading by name method
     *      LoadByPathMethod(...)    - Internal loading by path method
     *      LoadDirectoryMethod()    - Internal directory loading method
     *      
     *  - Protected methods ---- (for child classes when inheriting)
     *  
     *      GeneratePath(...)        - Generates a path for a file based on a given file-name
     *
     *  ------------------------
     *  
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
            StaticCommands.MakeSureDirectoryExists(directoryPath);
        }

        /// <summary>
        /// Saves an object to a file
        /// </summary>
        /// <param name="toSave">Object to save</param>
        public void Save(T toSave)
        {
            SaveMethod(toSave);
            Events.OnSaved(toSave);
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
            return load;
        }

        /// <summary>
        /// Returns all file paths of files that have been saved by the save system
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllFilePaths() =>
            StaticCommands.GetDirectoryFilePaths(DirectoryPath, FilesPrefix);

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
            StaticCommands.FileDelete(filePath);
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
        /// Checks if there is an existing saved file on a given path
        /// </summary>
        /// <param name="filePath">The path of the file</param>
        /// <returns></returns>
        public static bool FileExistsByPath(string filePath) =>
            StaticCommands.FileExists(filePath);

        /// <summary>
        /// Generates a path for a file based on a given file-name
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns></returns>
        public string GeneratePath(string fileName) =>
            PathGenerator.GeneratePathFile(DirectoryPath, FilesPrefix, fileName, FileType);

        /// <inheritdoc cref="PathGenerator.ExtractNameFromPath(string, string, string, string)" />
        public string ExtractName(string filePath) =>
            PathGenerator.ExtractNameFromPath(DirectoryPath, FilesPrefix, FileType, filePath);

        protected abstract void SaveMethod(T toSave);
        protected abstract T LoadByPathMethod(string filePath);
        protected abstract List<T> LoadAllFilesMethod();
    }
}
