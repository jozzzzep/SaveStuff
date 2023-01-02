namespace SavesAPI
{
    public class JsonSaveSystem<T> : SaveSystem<T> where T : class, ISaveable
    {
        public override string FileType => "json";

        public JsonSaveSystem(string folderPath, string filesPrefix, bool syncWebGL = false)
            : base(folderPath, filesPrefix, syncWebGL)
        { }
        
        public override void Save(string fileName, T toSave) =>
            StaticCommands.JsonSave(GeneratePath(fileName), toSave, syncWebGL);

        public override T Load(string fileName) =>
            StaticCommands.JsonLoad<T>(GeneratePath(fileName));

        public override T[] LoadDirectory() =>
            StaticCommands.JsonLoadDirectory<T>(FolderPath, FilesPrefix, syncWebGL);
    }
}
