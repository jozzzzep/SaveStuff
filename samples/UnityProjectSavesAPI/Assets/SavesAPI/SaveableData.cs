using SavesAPI.Advanced;
using System;

namespace SavesAPI
{
    [Serializable]
    public class SaveableData<T> where T : class, ISaveable
    {
        private T data;

        public static string ParentDirectoryPath => SaveableDataManager.DirectoryPath;
        public static string FilePrefix => SaveableDataManager.FilesPrefix;

        public string FilePath => SaveableDataManager.GeneratePath(this);

        public string FileType => SavingMethod == SavingMethod.Encrypted ? "data" : "json";

        public SavingMethod SavingMethod { get; private set; }

        public T Data 
        {
            get => data ?? Load();
            set
            {
                data = value;
                Save();
            }
        }

        public SaveableData(T data, SavingMethod savingMethod = SavingMethod.Json)
        {
            this.data = data;
            SavingMethod = savingMethod;
        }
 
        public void Save() => 
            SaveableDataManager.Save(this);

        public T Load() => 
            data = SaveableDataManager.Load(this);

        public bool FileExists() =>
            StaticSaveSystem.FileExists(FilePath);

        public void DeleteSavedFile() =>
            StaticSaveSystem.FileDelete(FilePath);
    }
}
