using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;

namespace SavesAPI
{
    public static class StaticCommands
    {
        public static bool FileExists(string filePath) => File.Exists(filePath);

        public static void FileDelete(string filePath, bool syncWebGL = false)
        {
            if (FileExists(filePath))
            {
                File.Delete(filePath);
                if (syncWebGL)
                    WebGLFileSync();
            }
        }

        public static void MakeSureDirectoryExists(string folderPath, bool syncWebGL = false)
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                if (syncWebGL)
                    WebGLFileSync();
            }
        }

        public static T[] LoadDirectory<T>
            (string folderPath, string filePrefix, Func<string, T> loadFunction, bool syncWebGL = false)
            where T : class, ISaveable
        {
            MakeSureDirectoryExists(folderPath, syncWebGL);

            var objectsLoaded = new List<T>();
            var filePaths = Directory.GetFiles(folderPath);

            for (int i = 0; i < filePaths.Length; i++)
                if (filePaths[i].Contains(filePrefix))
                    objectsLoaded.Add(loadFunction(filePaths[i]));

            return objectsLoaded.ToArray();
        }

        public static string GeneratePathFile(string folderPath, string prefix, string name, string fileType)
            => $"{folderPath}{prefix}-{name}.{fileType}";

        public static string GeneratePathSubfolder(string mainFolderPath, string subfolderName)
            => $"{mainFolderPath}\\{subfolderName}\\";

        public static void EncryptedSave<Type>(string path, Type objectToSave, bool syncWebGL = false)
        {
            if (FileExists(path))
                FileDelete(path);

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, objectToSave);
            stream.Close(); // IMPORTANT

            if (syncWebGL)
                WebGLFileSync();
        }

        public static T EncryptedLoad<T>(string path)
            where T : class, ISaveable
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            T data = formatter.Deserialize(stream) as T;
            stream.Close(); // IMPORTANT
            return data;
        }

        public static T[] EncryptedLoadDirectory<T>(string folderPath, string filePrefix, bool syncWebGL = false)
            where T : class, ISaveable =>
            LoadDirectory(folderPath, filePrefix, EncryptedLoad<T>, syncWebGL);

        public static void JsonSave(string path, object obj, bool syncWebGL = false)
        {
            var toJson = JsonUtility.ToJson(obj, true);
            File.WriteAllText(path, toJson);

            if (syncWebGL)
                WebGLFileSync();
        }

        public static T JsonLoad<T>(string path)
            where T : class, ISaveable
        {
            var raw = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(raw);
        }

        public static T[] JsonLoadDirectory<T>(string folderPath, string filePrefix, bool syncWebGL = false)
            where T : class, ISaveable =>
            LoadDirectory(folderPath, filePrefix, JsonLoad<T>, syncWebGL);

        /// <summary>
        /// Calls the file sync function in WebGL
        /// </summary>
        public static void WebGLFileSync()
        {
#if UNITY_WEBGL
        Application.ExternalEval("_JS_FileSystem_Sync();");
#endif
        }
    }
}
