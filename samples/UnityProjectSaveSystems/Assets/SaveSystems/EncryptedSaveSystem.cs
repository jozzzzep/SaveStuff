namespace SavesAPI
{
    public class EncryptedSaveSystem<T> : SaveSystem<T> where T : class, ISaveable
    {
        public override string FileType => fileType;
        private string fileType;

        public EncryptedSaveSystem(string folderPath, string filesPrefix, string fileType, bool syncWebGL = false)
            : base(folderPath, filesPrefix, syncWebGL) => 
            this.fileType = fileType;

        public override void Save(string fileName, T toSave) =>
            StaticCommands.EncryptedSave(GeneratePath(fileName), toSave, syncWebGL);

        public override T Load(string fileName) =>
            StaticCommands.EncryptedLoad<T>(GeneratePath(fileName));

        public override T[] LoadDirectory() =>
            StaticCommands.EncryptedLoadDirectory<T>(FolderPath, FilesPrefix, syncWebGL);
    }
}
