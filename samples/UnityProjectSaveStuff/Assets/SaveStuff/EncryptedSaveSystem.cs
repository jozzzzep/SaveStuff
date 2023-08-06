using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SavesStuff.Advanced;

namespace SavesStuff
{
    /*/
     * 
     *  - Properties ----------------
     *  
     *      FileType                - The file type of the saveable files
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
     *      StaticSave(...)         - Will save a saveable object to a file and encrypt it
     *      StaticLoad(...)         - Will decrypt and load a saveable file
     *      
    /*/

    /// <summary>
    /// A save system that saves an object to an encrypted file, and decrypts it when loading
    /// </summary>
    /// <typeparam name="T">A saveable type class</typeparam>
    public class EncryptedSaveSystem<T> : SaveSystem<T> where T : class, ISaveable
    {
        public override string FileType => fileType;
        private string fileType;

        /// <summary>
        /// A constructor for creating an encrypted save system
        /// </summary>
        /// <param name="directoryPath">The directory path the save system saves-to and loads-from</param>
        /// <param name="filesPrefix">The prefix of every file created with the save system</param>
        /// <param name="fileType">The file type of the saveable files</param>
        public EncryptedSaveSystem(string directoryPath, string filesPrefix, string fileType)
            : base(directoryPath, filesPrefix) => 
            this.fileType = fileType;

        /// <summary>
        /// Save system constructor for basic usage
        /// </summary>
        /// <param name="directoryName">The name of the saves directory</param>
        public EncryptedSaveSystem(string directoryName)
            : base(directoryName)
        { }

        protected override void SaveMethod(T toSave) =>
            StaticSave(GeneratePath(toSave.Name), toSave);

        protected override T LoadByPathMethod(string filePath) =>
            StaticLoad(filePath);

        /// <summary>
        /// Will save a saveable object to a file and encrypt it
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <param name="objectToSave">Object to save and encrypt</param>
        public static void StaticSave(string path, T objectToSave)
        {
            FileDelete(path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            formatter.Serialize(stream, objectToSave);
            stream.Close(); // IMPORTANT
        }

        /// <summary>
        /// Will decrypt and load a saveable file
        /// </summary>
        /// <param name="path">Path of saveable file</param>
        /// <returns></returns>
        public static T StaticLoad(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            T data = formatter.Deserialize(stream) as T;
            stream.Close(); // IMPORTANT
            return data;
        }
    }
}
