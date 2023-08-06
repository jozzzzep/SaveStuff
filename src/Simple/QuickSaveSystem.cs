using SavesStuff.Advanced;
using System;

namespace SavesStuff
{
    /*/
     * 
     *  - Properties ----------------
     *  
     *      DirectoryPath           - The directory path the save system saves to and loads from
     *      FilesPrefix             - The prefix of every file created with the save system
     *      
     *  - Methods -------------------
     *  
     *      Save(...)               - Saves an object to a file
     *      Load(...)               - Loads an object from a file
     *      Delete(...)             - Deletes a saved file
     *      
     *      LoadIfExists(...)       - Loads a file only if it exits
     *      FileExists(...)         - Checks if there is an existing saved file with a chosen name
     *      
     *      GeneratePath(...)       - Generates a path for a file based on a given file-name
     *      ExtractName(...)        - Extracts the file name from a file path
     *      GetFileType(...)        - Returns the file tyoe of a given saveable object
     *      
     *      InitializeObject(...)   - If there is an existing save for the object - returns the loaded object data from the file
     *                                If there is not an existing file - returns the given default value
     *      
    /*/

    /// <summary>
    /// An easy-to-use static save system, for saving and loading without a save system instance <br></br>
    /// The system takes objects that implement the <see cref="IQuickSaveable"/> interface
    /// </summary>
    public static class QuickSaveSystem
    {
        private static string directoryPath;

        /// <inheritdoc cref="SaveSystem{T}.DirectoryPath"/>
        public static string DirectoryPath { 
            get => directoryPath ?? 
                (DirectoryPath = PathGenerator.GeneratePathDirectory("general"));
            set
            {
                SaveSystem<ISaveable>.MakeSureDirectoryExists(value);
                directoryPath = value;
            }
        }

        private static string filesPrefix;

        /// <inheritdoc cref="SaveSystem{T}.FilesPrefix"/>
        public static string FilesPrefix 
        { 
            get => filesPrefix ?? (filesPrefix = "generalData"); 
            set => filesPrefix = value; 
        }

        /// <inheritdoc cref="SaveSystem{T}.Save(T)"/>
        public static void Save<T>(T s) where T : class, IQuickSaveable
        {
            Action<string, T> saveAction =
                s.SavingMethod == SavingMethod.Encrypted ?
                EncryptedSaveSystem<T>.StaticSave :
                JsonSaveSystem<T>.StaticSave;
            saveAction(GeneratePath(s), s);
        }

        /// <inheritdoc cref="SaveSystem{T}.Load(string)"/>
        public static T Load<T>(T s) where T : class, IQuickSaveable
        {
            Func<string, T> func =
                s.SavingMethod == SavingMethod.Encrypted ?
                EncryptedSaveSystem<T>.StaticLoad :
                JsonSaveSystem<T>.StaticLoad;
            return func(GeneratePath(s));
        }

        /// <inheritdoc cref="SaveSystem{T}.Delete(string)"/>
        public static void Delete<T>(T obj) where T : class, IQuickSaveable 
            => SaveSystem<ISaveable>.FileDelete(GeneratePath(obj));

        /// <inheritdoc cref="SaveSystem{T}.LoadIfExists(string)"/>
        public static T LoadIfExist<T>(T s) where T : class, IQuickSaveable =>
            FileExists(s) ? Load(s) : null;

        /// <summary>
        /// Checks if there is already an existing saved file for a given object
        /// </summary>
        public static bool FileExists<T>(T s) where T : class, IQuickSaveable =>
            SaveSystem<T>.FileExistsByPath(GeneratePath(s));

        /// <inheritdoc cref="PathGenerator.GeneratePathFile(string, string, string, string)"/>
        public static string GeneratePath(string fileName, string fileType)=>
            PathGenerator.GeneratePathFile(DirectoryPath, FilesPrefix, fileName, fileType);

        /// <inheritdoc cref="PathGenerator.GeneratePathFile(string, string, string, string)"/>
        public static string GeneratePath<T>(T savableObj) where T : class, IQuickSaveable =>
            GeneratePath(savableObj.Name, GetFileType(savableObj));

        /// <summary>
        /// Returns the file type of a given saveable object
        /// </summary>
        public static string GetFileType<T>(T obj) where T : class, IQuickSaveable =>
            obj.SavingMethod == SavingMethod.Encrypted ? "savedata" : "json";

        /// <summary>
        /// If there is an existing save for the object - returns the loaded object data from the file <br></br>
        /// If there is not an existing file - returns the given default value
        /// </summary>
        /// <param name="defaultData">The default value of the save file</param>
        /// <returns></returns>
        public static T InitializeObject<T>(T defaultData) where T : class, IQuickSaveable =>
            LoadIfExist(defaultData) ?? defaultData;
    }
}
