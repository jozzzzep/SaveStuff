using System;

namespace SavesAPI
{
    public abstract class SaveSystem<T> where T : class, ISaveable
    {
        public abstract string FileType { get; }
        public string FolderPath { get; private set; }
        public string FilesPrefix { get; private set; }

        public SaveSystem(string folderPath, string filesPrefix)
        {
            FolderPath = folderPath;
            FilesPrefix = filesPrefix;
        }

        public abstract void Save(string filename, T toSave);
        public abstract T Load(string filename);
        public abstract T[] LoadDirectory();

        public void Delete(string filename) =>
            StaticCommands.FileDelete(GeneratePath(filename));

        public T LoadIfExists(string fileName)
        {
            if (FileExists(fileName))
                return Load(fileName);
            return null;
        }

        public bool FileExists(string fileName) =>
            StaticCommands.FileExists(GeneratePath(fileName));

        protected string GeneratePath(string fileName) =>
            StaticCommands.GeneratePathFile(FolderPath, FilesPrefix, fileName, FileType);
    }
}
