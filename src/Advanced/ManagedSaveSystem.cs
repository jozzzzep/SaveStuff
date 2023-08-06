namespace SavesStuff.Advanced
{
    /*/ 
     * 
     *  - Members ----------------
     *  
     *      InternalSaveSystem  - The internal save system
     *      FileType            - The file type of the saveable files
     *      DirectoryPath       - The directory path the save system saves to and loads from
     *      FilesPrefix         - The prefix of every file created with the save system
     *      Events              - Events of the managed save system
     *      
     *  - Protected members ------ (for child classes when inheriting)
     *  
     *      internalSaveSystem  - The internal save system
     *      
    /*/

    /// <summary>
    /// A base class for creating managed save systems
    /// </summary>
    /// <typeparam name="T">A saveable type class</typeparam>
    public class ManagedSaveSystem<T> where T : class, ISaveable
    {
        /// <summary>
        /// The internal save system, you can choose which save system you want to use
        /// </summary>
        public SaveSystem<T> InternalSaveSystem { get; private set; }

        /// <inheritdoc cref="SaveSystem{T}.Events" />
        public SaveSystemEvents<T> Events  => InternalSaveSystem.Events;

        /// <inheritdoc cref="SaveSystem{T}.FileType" />
        public string FileType => InternalSaveSystem.FileType;

        /// <inheritdoc cref="SaveSystem{T}.DirectoryPath" />
        public string DirectoryPath => InternalSaveSystem.DirectoryPath;

        /// <inheritdoc cref="SaveSystem{T}.FilesPrefix" />
        public string FilesPrefix => InternalSaveSystem.FilesPrefix;

        /// <summary>
        /// The base constructor of a managed save system
        /// </summary>
        /// <param name="internalSaveSystem"></param>
        public ManagedSaveSystem(SaveSystem<T> internalSaveSystem)
        {
            InternalSaveSystem = internalSaveSystem;
        }
    }
}
