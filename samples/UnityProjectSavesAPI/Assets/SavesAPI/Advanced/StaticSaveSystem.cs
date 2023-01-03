using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;

namespace SavesAPI.Advanced
{
    /// NOT RECOMMENDED TO USE DIRECTLY - Read documentation and examples here:
    /// https://github.com/jozzzzep/SavesAPI

    /// - Methods ---------------------------
    ///    FileExists(...)                - Checks if a file exists in a path
    ///    FileDelete(...)                - Deletes a file from a path only if it exists
    ///    LoadDirectory<T>(...)          - Loads all saveable files from a chosen directory, if empty - list count will be 0
    ///   --
    ///    EncryptedSave<T>(...)          - Will save a saveable object to a file and encrypt it
    ///    EncryptedLoad<T>(...)          - Will decrypt and load a saveable file
    ///    EncryptedLoadDirectory<T>(...) - Will decrypt and load all saveable files in a chosen directory
    ///   --
    ///    JsonSave<T>(...)               - Will save a saveable object and serialize it to json format
    ///    JsonLoad<T>(...)               - Will load a file and deserialize it from json to saveable type
    ///    JsonLoadDirectory<T>(...)      - Will deserialize and load all saveable json files in a chosen directory
    /// -------------------------------------

    /// <summary>
    /// Static commands of the SavesAPI, not recommended to use manually
    /// </summary>
    public static class StaticSaveSystem
    {
        /// <summary>
        /// Checks if a file exists in a path
        /// </summary>
        /// <param name="filePath">Path to check</param>
        /// <returns></returns>
        public static bool FileExists(string filePath) => File.Exists(filePath);

        /// <summary>
        /// Deletes a file from a path only if it exists
        /// </summary>
        /// <param name="filePath">Path of file</param>
        public static void FileDelete(string filePath)
        {
            if (FileExists(filePath))
            {
                File.Delete(filePath);
                WebGLFileSync();
            }
        }

        /// <summary>
        /// Loads all saveable files from a chosen directory, if empty - list count will be 0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folderPath">Saveable files directory to load from</param>
        /// <param name="filePrefix">Prefix of saveable files</param>
        /// <param name="loadFunction">Load fucnction based on the saveable file type</param>
        /// <returns>A list of all the loaded saveable files</returns>
        public static List<T> LoadDirectory<T>
            (string folderPath, string filePrefix, Func<string, T> loadFunction)
            where T : class, ISaveable
        {
            MakeSureDirectoryExists(folderPath);

            var objectsLoaded = new List<T>();
            var filePaths = Directory.GetFiles(folderPath);

            for (int i = 0; i < filePaths.Length; i++)
                if (filePaths[i].Contains(filePrefix))
                    objectsLoaded.Add(loadFunction(filePaths[i]));

            return objectsLoaded;
        }

        
        /// <summary>
        /// Will save a saveable object to a file and encrypt it, can be loaded only with <see cref="EncryptedLoad{T}(string)"/>
        /// </summary>
        /// <typeparam name="T">Type of saveable object</typeparam>
        /// <param name="path">Path of the file</param>
        /// <param name="objectToSave">Object to save and encrypt</param>
        public static void EncryptedSave<T>(string path, T objectToSave)
            where T : class, ISaveable
        {
            if (FileExists(path))
                FileDelete(path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, objectToSave);
            stream.Close(); // IMPORTANT

            WebGLFileSync();
        }

        /// <summary>
        /// Will decrypt and load a saveable file
        /// </summary>
        /// <typeparam name="T">Type of saveable file</typeparam>
        /// <param name="path">Path of saveable file</param>
        /// <returns></returns>
        public static T EncryptedLoad<T>(string path)
            where T : class, ISaveable
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            T data = formatter.Deserialize(stream) as T;
            stream.Close(); // IMPORTANT
            data.LastUsage = GetLastModificationTime(path);
            return data;
        }

        /// <summary>
        /// Will decrypt and load all saveable files in a chosen directory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="folderPath">Path of diectory to load from</param>
        /// <param name="filePrefix">Prefix of saveable files</param>
        /// <returns></returns>
        public static List<T> EncryptedLoadDirectory<T>(string folderPath, string filePrefix)
            where T : class, ISaveable =>
            LoadDirectory(folderPath, filePrefix, EncryptedLoad<T>);

        /// <summary>
        /// Will save a saveable object and serialize it to json format
        /// </summary>
        /// <param name="path">Pathof the file</param>
        /// <param name="obj">Saveable object</param>
        public static void JsonSave<T>(string path, T obj)
             where T : class, ISaveable
        {
            if (FileExists(path))
                FileDelete(path);

            var toJson = JsonUtility.ToJson(obj, true);
            File.WriteAllText(path, toJson);
            WebGLFileSync();
        }

        /// <summary>
        /// Will load a file and deserialize it from json to saveable type
        /// </summary>
        /// <typeparam name="T">Saveable type</typeparam>
        /// <param name="path">Path to load from</param>
        /// <returns></returns>
        public static T JsonLoad<T>(string path)
            where T : class, ISaveable
        {
            var raw = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(raw);
            data.LastUsage = GetLastModificationTime(path);
            return data;
        }

        /// <summary>
        /// Will deserialize and load all saveable files in a chosen directory
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="directoryPath">Directory path to load from</param>
        /// <param name="filePrefix">Saveable files prefix</param>
        /// <returns></returns>
        public static List<T> JsonLoadDirectory<T>(string directoryPath, string filePrefix)
            where T : class, ISaveable =>
            LoadDirectory(directoryPath, filePrefix, JsonLoad<T>);

        /// <summary>
        /// Will return the last time a file has been modified or created
        /// </summary>
        /// <param name="path">Path of file</param>
        /// <returns></returns>
        private static DateTime GetLastModificationTime(string path) => File.GetLastWriteTime(path);

        /// <summary>
        /// Creates a directory in a chosen path if it does not exist
        /// </summary>
        /// <param name="dirPath">The path of the directory</param>
        private static void MakeSureDirectoryExists(string dirPath)
        {
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                WebGLFileSync();
            }
        }

        /// <summary>
        /// Calls the file sync function in WebGL
        /// </summary>
        private static void WebGLFileSync()
        {
#if UNITY_WEBGL
        Application.ExternalEval("_JS_FileSystem_Sync();");
#endif
        }
    }
}
