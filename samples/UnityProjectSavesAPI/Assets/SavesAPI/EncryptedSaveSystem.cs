using System.Collections.Generic;
using SavesAPI.Advanced;

namespace SavesAPI
{
    /// - Properties -----------
    ///     FileType           - The file type of the saveable files
    ///     DirectoryPath      - The directory path the save system saves to and loads from
    ///     FilesPrefix        - The prefix of every file created with the save system
    ///     
    /// - Methods --------------
    ///     Save(...)          - Saves an object to a file
    ///     Delete(...)        - Deletes a saved file
    ///     Load(...)          - Loads an object from a file
    ///     LoadIfExists(...)  - Loads a file only if it exits
    ///     LoadDirectory()    - Loads all saved files and returns the objects they store
    ///     FileExists(...)    - Checks if there is an existing saved file with a chosen name

    /// <summary>
    /// A save system that saves an object to an encrypted file, and decrypts it when loading
    /// </summary>
    /// <typeparam name="T">A saveable type class</typeparam>
    public class EncryptedSaveSystem<T> : SaveSystem<T> where T : class, ISaveable
    {
        public override string FileType => fileType;
        private string fileType;

        /// <summary>
        /// A constructor for creating an encrypted save system
        /// </summary>
        /// <param name="directoryPath">The directory path the save system saves-to and loads-from</param>
        /// <param name="filesPrefix">The prefix of every file created with the save system</param>
        /// <param name="fileType">The file type of the saveable files</param>
        public EncryptedSaveSystem(string directoryPath, string filesPrefix, string fileType)
            : base(directoryPath, filesPrefix) => 
            this.fileType = fileType;

        public override void Save(T toSave) =>
            StaticSaveSystem.EncryptedSave(GeneratePath(toSave.Name), toSave);

        public override T Load(string fileName) =>
            StaticSaveSystem.EncryptedLoad<T>(GeneratePath(fileName));

        public override List<T> LoadDirectory() =>
            StaticSaveSystem.EncryptedLoadDirectory<T>(DirectoryPath, FilesPrefix);
    }
}
