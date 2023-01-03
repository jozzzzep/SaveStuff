namespace SavesAPI.Advanced
{
    /// WARNING: use only to inherit and create custom managed save system
    /// 
    /// - Members ----------------
    /// 
    ///     FileType           - The file type of the saveable files
    ///     DirectoryPath      - The directory path the save system saves to and loads from
    ///     FilesPrefix        - The prefix of every file created with the save system <summary>
    ///     
    /// - Protected members ------ (for child classes when inheriting)
    /// 
    ///     internalSaveSystem - The internal save system
    /// 
    /// --------------------------

    /// <summary>
    /// A base class for creating managed save systems
    /// </summary>
    /// <typeparam name="T">A saveable type class</typeparam>
    public class ManagedSaveSystem<T> where T : class, ISaveable
    {
        /// <summary>
        /// The internal save system, you can choose which save system you want to use
        /// </summary>
        protected SaveSystem<T> internalSaveSystem;

        /// <summary>
        /// File type of saveable files
        /// </summary>
        public string FileType => internalSaveSystem.FileType;

        /// <summary>
        /// The directory path the save system saves-to and loads-from
        /// </summary>
        public string DirectoryPath => internalSaveSystem.DirectoryPath;

        /// <summary>
        /// The prefix of every file created with the save system
        /// </summary>
        public string FilesPrefix => internalSaveSystem.FilesPrefix;

        /// <summary>
        /// The base constructor of a managed save system
        /// </summary>
        /// <param name="internalSaveSystem"></param>
        public ManagedSaveSystem(SaveSystem<T> internalSaveSystem) =>
            this.internalSaveSystem = internalSaveSystem;
    }
}
