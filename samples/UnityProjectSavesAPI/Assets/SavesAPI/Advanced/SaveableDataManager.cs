using Unity.VisualScripting;
using System;

namespace SavesAPI.Advanced
{
    public static class SaveableDataManager
    {
        private static string directoryPath;
        public static string DirectoryPath { 
            get => directoryPath ?? PathGenerator.GeneratePathDirectory("general");
            set => directoryPath = value;
        }

        public static string FilesPrefix => "generalData";

        public static void Save<T>(SaveableData<T> s) where T : class, ISaveable
        {
            Action<string, T> saveAction =
                s.SavingMethod == SavingMethod.Encrypted ?
                StaticSaveSystem.EncryptedSave :
                StaticSaveSystem.JsonSave;
            saveAction(s.FilePath, s.Data);
        }

        public static T Load<T>(SaveableData<T> s) where T : class, ISaveable
        {
            Func<string, T> func =
                s.SavingMethod == SavingMethod.Encrypted ?
                StaticSaveSystem.EncryptedLoad<T> :
                StaticSaveSystem.JsonLoad<T>;
            return func(s.FilePath);
        }

        public static string GeneratePath<T>(SaveableData<T> savableData) where T : class, ISaveable =>
            PathGenerator.GeneratePathFile(DirectoryPath, FilesPrefix, savableData.Data.Name, savableData.FileType);
    }
}
