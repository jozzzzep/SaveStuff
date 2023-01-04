using SavesAPI.Advanced;
using System;
using Unity.VisualScripting;

namespace SavesAPI
{
    [Serializable]
    public abstract class SaveableObject : ISaveable
    {
        abstract public string Name { get; }
        abstract public SavingMethod SavingMethod { get; }

        public string FilePath => SaveableDataManager.GeneratePath(this);
        public string FileType => SavingMethod == SavingMethod.Encrypted ? "data" : "json";

        public DateTime LastUsage { get; set; }

        public static string ParentDirectoryPath => SaveableDataManager.DirectoryPath;
        public static string FilePrefix => SaveableDataManager.FilesPrefix;

        public bool FileExists() =>
            StaticSaveSystem.FileExists(FilePath);
    }

    public class ObjectSaver<T> where T : SaveableObject
    {
        private T data;

        public T Data 
        {
            get => data ?? Load();
            set
            {
                
                data = value;
                Save();
            }
        }

        public ObjectSaver(T data) =>
            Data = data;
 
        public void Save() => 
            SaveableDataManager.Save(this);

        public T Load(T data) => 
            SaveableDataManager.Load(data);

        public T Load() =>
            data = Load(data);

        public T LoadIfExist
    }
}
