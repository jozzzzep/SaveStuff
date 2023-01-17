using SavesAPI.Advanced;
using System;

namespace SavesAPI
{
    /// <summary>
    /// An easy-to-use static save system, for saving and loading without a save system instance <br></br>
    /// The system takes objects that implement the <see cref="ISaveableObject"/> interface
    /// </summary>
    public static class ObjectsSaveSystem
    {
        private static string directoryPath;

        /// <inheritdoc cref="SaveSystem{T}.DirectoryPath"/>
        public static string DirectoryPath { 
            get => directoryPath ?? 
                (DirectoryPath = PathGenerator.GeneratePathDirectory("general"));
            set
            {
                StaticCommands.MakeSureDirectoryExists(value);
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
        public static void Save<T>(T s) where T : class, ISaveableObject
        {
            Action<string, T> saveAction =
                s.SavingMethod == SavingMethod.Encrypted ?
                StaticCommands.EncryptedSave :
                StaticCommands.JsonSave;
            saveAction(GeneratePath(s), s);
        }

        /// <inheritdoc cref="SaveSystem{T}.Load(string)"/>
        public static T Load<T>(T s) where T : class, ISaveableObject
        {
            Func<string, T> func =
                s.SavingMethod == SavingMethod.Encrypted ?
                StaticCommands.EncryptedLoad<T> :
                StaticCommands.JsonLoad<T>;
            return func(GeneratePath(s));
        }

        /// <summary>
        /// Checks if there is already an existing saved file for a given object
        /// </summary>
        public static bool FileExists<T>(T s) where T : class, ISaveableObject =>
            StaticCommands.FileExists(GeneratePath(s));

        /// <inheritdoc cref="SaveSystem{T}.LoadIfExists(string)"/>
        public static T LoadIfExist<T>(T s) where T : class, ISaveableObject =>
            FileExists(s) ? Load(s) : null;

        /// <inheritdoc cref="PathGenerator.GeneratePathFile(string, string, string, string)"/>
        public static string GeneratePath(string fileName, string fileType)=>
            PathGenerator.GeneratePathFile(DirectoryPath, FilesPrefix, fileName, fileType);

        /// <inheritdoc cref="PathGenerator.GeneratePathFile(string, string, string, string)"/>
        public static string GeneratePath<T>(T savableObj) where T : class, ISaveableObject =>
            GeneratePath(savableObj.Name, GetFileType(savableObj));

        /// <summary>
        /// Returns the file type of a given saveable object
        /// </summary>
        public static string GetFileType<T>(T obj) where T : class, ISaveableObject =>
            obj.SavingMethod == SavingMethod.Encrypted ? "savedata" : "json";

        /// <summary>
        /// If there is an existing save for the object - returns the loaded object from the file <br></br>
        /// If there is not an existing file - returns the given default value
        /// </summary>
        /// <param name="defaultData">The default value of the save file</param>
        /// <returns></returns>
        public static T InitializeObject<T>(T defaultData) where T : class, ISaveableObject =>
            LoadIfExist(defaultData) ?? defaultData;
    }
}
