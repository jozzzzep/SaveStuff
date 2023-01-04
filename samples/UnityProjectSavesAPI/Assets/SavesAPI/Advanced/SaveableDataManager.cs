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

        public static void Save<T>(T s) where T : SaveableObject
        {
            Action<string, T> saveAction =
                s.SavingMethod == SavingMethod.Encrypted ?
                StaticSaveSystem.EncryptedSave :
                StaticSaveSystem.JsonSave;
            saveAction(s.FilePath, s);
        }

        public static T Load<T>(T s) where T : SaveableObject
        {
            Func<string, T> func =
                s.SavingMethod == SavingMethod.Encrypted ?
                StaticSaveSystem.EncryptedLoad<T> :
                StaticSaveSystem.JsonLoad<T>;
            return func(s.FilePath);
        }
        public static string GeneratePath(string fileName, string fileType)=>
            PathGenerator.GeneratePathFile(DirectoryPath, FilesPrefix, fileName, fileType);

        public static string GeneratePath<T>(T savableObj) where T : SaveableObject =>
            GeneratePath(savableObj.Name, savableObj.FileType);
    }
}
