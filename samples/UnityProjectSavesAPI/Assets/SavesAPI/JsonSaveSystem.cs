using System.Collections.Generic;
using SavesAPI.Advanced;

namespace SavesAPI
{
    /*/
     * 
     *  - Properties -----------
     *      FileType           - The file type of the saveable files - Always "json"
     *      DirectoryPath      - The directory path the save system saves to and loads from
     *      FilesPrefix        - The prefix of every file created with the save system
     *      
     *  - Methods --------------
     *      Save(...)          - Saves an object to a file
     *      Delete(...)        - Deletes a saved file
     *      Load(...)          - Loads an object from a file
     *      LoadIfExists(...)  - Loads a file only if it exits
     *      LoadDirectory()    - Loads all saved files and returns the objects they store
     *      FileExists(...)    - Checks if there is an existing saved file with a chosen name
     *      
    /*/

    /// <summary>
    /// A save system that saves an object to a file and serializes it to json format
    /// </summary>
    /// <typeparam name="T">A saveable type class</typeparam>
    public class JsonSaveSystem<T> : SaveSystem<T> where T : class, ISaveable
    {
        /// <summary>
        /// The file type is always "json" in the class <see cref="JsonSaveSystem{T}"/>
        /// </summary>
        public override string FileType => "json";

        /// <summary>
        /// A constructor for creating a json save system
        /// </summary>
        /// <param name="directoryPath">The directory path the save system saves-to and loads-from</param>
        /// <param name="filesPrefix">The prefix of every file created with the save system</param>
        public JsonSaveSystem(string directoryPath, string filesPrefix)
            : base(directoryPath, filesPrefix)
        { }
        
        public override void Save(T toSave) =>
            StaticCommands.JsonSave(GeneratePath(toSave.Name), toSave);

        public override T Load(string fileName) =>
            StaticCommands.JsonLoad<T>(GeneratePath(fileName));

        public override List<T> LoadDirectory() =>
            StaticCommands.JsonLoadDirectory<T>(DirectoryPath, FilesPrefix);
    }
}
