using System.Collections.Generic;

namespace SavesAPI.Advanced
{
    /// WARNING: use only to inherit and create custom save system
    ///
    /// - Members --------------
    /// 
    ///     DirectoryPath      - The directory path the save system saves to and loads from
    ///     FilesPrefix        - The prefix of every file created with the save system
    ///     Delete(...)        - Deletes a saved file
    ///     LoadIfExists(...)  - Loads a file only if it exits
    ///     FileExists(...)    - Checks if there is an existing saved file with a chosen name
    /// 
    /// - Abstract members ----- (implement when inheriting) 
    /// 
    ///     FileType           - The file type of the saveable files
    ///     Save(...)          - Saves an object to a file
    ///     Load(...)          - Loads an object from a file
    ///     LoadDirectory()    - Loads all saved files and returns the objects they store
    ///     
    /// - Protected methods ---- (for child classes when inheriting)
    /// 
    ///     GeneratePath(...)  - Generates a path for a file based on a given file-name
    ///     
    /// ------------------------

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
        /// The directory path the save system saves to and loads from
        /// </summary>
        public string DirectoryPath { get; private set; }

        /// <summary>
        /// The prefix of every file created with the save system
        /// </summary>
        public string FilesPrefix { get; private set; }

        /// <summary>
        /// A constructor for the base save system
        /// </summary>
        /// <param name="directoryPath">The directory path the save system saves to and loads from</param>
        /// <param name="filesPrefix">The prefix of every file created with the save system</param>
        public SaveSystem(string directoryPath, string filesPrefix)
        {
            DirectoryPath = directoryPath;
            FilesPrefix = filesPrefix;
            StaticSaveSystem.MakeSureDirectoryExists(directoryPath);
        }

        /// <summary>
        /// Saves an object to a file
        /// </summary>
        /// <param name="filename">The name of the file</param>
        /// <param name="toSave">Object to save</param>
        public abstract void Save(T toSave);

        /// <summary>
        /// Loads an object from a file
        /// </summary>
        /// <param name="filename">The name of the file</param>
        /// <returns></returns>
        public abstract T Load(string filename);

        /// <summary>
        /// Loads all saved files and returns the objects they store
        /// </summary>
        /// <returns></returns>
        public abstract List<T> LoadDirectory();

        /// <summary>
        /// Deletes a saved file
        /// </summary>
        /// <param name="filename"></param>
        public void Delete(string filename) =>
            StaticSaveSystem.FileDelete(GeneratePath(filename));

        /// <summary>
        /// Loads a file only if it exits
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns><see cref="null"/> if file does not exist</returns>
        public T LoadIfExists(string fileName)
        {
            if (FileExists(fileName))
                return Load(fileName);
            return null;
        }

        /// <summary>
        /// Checks if there is an existing saved file with a chosen name
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns></returns>
        public bool FileExists(string fileName) =>
            StaticSaveSystem.FileExists(GeneratePath(fileName));

        /// <summary>
        /// Generates a path for a file based on a given file-name
        /// </summary>
        /// <param name="fileName">The name of the file</param>
        /// <returns></returns>
        protected string GeneratePath(string fileName) =>
            PathGenerator.GeneratePathFile(DirectoryPath, FilesPrefix, fileName, FileType);
    }
}
