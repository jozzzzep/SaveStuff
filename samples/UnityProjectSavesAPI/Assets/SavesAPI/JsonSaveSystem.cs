using System.IO;
using SavesAPI.Advanced;
using UnityEngine;

namespace SavesAPI
{
    /*/
     * 
     *  - Properties ----------------
     *  
     *      FileType                - The file type of the saveable files - Always "json"
     *      DirectoryPath           - The directory path the save system saves to and loads from
     *      FilesPrefix             - The prefix of every file created with the save system
     *      Events                  - Events of the save system
     *      
     *  - Methods -------------------
     *  
     *      Save(...)               - Saves an object to a file
     *      Load(...)               - Loads an object from a file
     *      LoadByPath(...)         - Loads an object from a file
     *      Delete(...)             - Deletes a saved file
     *      DeleteByPath(...)       - Deletes a saved file
     *                             
     *      LoadIfExists(...)       - Loads a file only if it exits
     *      LoadIfExistsByPath(...) - Loads a file only if it exits
     *      FileExists(...)         - Checks if there is an existing saved file with a chosen name
     *      
     *      GetAllFilePaths()       - Returns all file paths of files that have been saved by the save system
     *      LoadMultiple(...)       - Loads multiple saved files
     *      LoadAllFiles()          - Loads all files that have been saved by the save system
     *      
     *      GeneratePath(...)       - Generates a path for a file based on a given file-name
     *      ExtractName(...)        - Extracts the file name from a file path
     *      
     *  - Static Methods ------------
     *  
     *      StaticSave(...)         - Will save a saveable object and serialize it to json format
     *      StaticLoad(...)         - Will load a file and deserialize it from json to a saveable type
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

        /// <summary>
        /// Save system constructor for basic usage
        /// </summary>
        /// <param name="directoryName">The name of the saves directory</param>
        public JsonSaveSystem(string directoryName) 
            : base(directoryName)
        { }
       
        protected override void SaveMethod(T toSave) =>
            StaticSave(GeneratePath(toSave.Name), toSave);

        protected override T LoadByPathMethod(string filePath) =>
            StaticLoad(filePath);

        /// <summary>
        /// Will save a saveable object and serialize it to json format
        /// </summary>
        /// <param name="path">Pathof the file</param>
        /// <param name="obj">Saveable object</param>
        public static void StaticSave(string path, T obj)
        {
            FileDelete(path);
            var toJson = JsonUtility.ToJson(obj, true);
            File.WriteAllText(path, toJson);
        }

        /// <summary>
        /// Will load a file and deserialize it from json to saveable type
        /// </summary>
        /// <typeparam name="T">Saveable type</typeparam>
        /// <param name="path">Path to load from</param>
        /// <returns></returns>
        public static T StaticLoad(string path)
        {
            var raw = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(raw);
            return data;
        }
    }
}
