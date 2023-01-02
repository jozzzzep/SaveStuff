using System;

namespace SavesAPI
{
    public abstract class SaveSystem<T> where T : class, ISaveable
    {
        public abstract string FileType { get; }
        public string FolderPath { get; private set; }
        public string FilesPrefix { get; private set; }

        protected bool syncWebGL;

        public SaveSystem(string folderPath, string filesPrefix, bool syncWebGL = false)
        {
            FolderPath = folderPath;
            FilesPrefix = filesPrefix;
            this.syncWebGL = syncWebGL;
        }

        public abstract void Save(string filename, T toSave);
        public abstract T Load(string filename);
        public abstract T[] LoadDirectory();

        public void Delete(string filename) =>
            StaticCommands.FileDelete(GeneratePath(filename), syncWebGL);

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
