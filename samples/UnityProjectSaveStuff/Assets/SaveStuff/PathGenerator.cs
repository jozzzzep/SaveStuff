﻿using UnityEngine;

namespace SavesStuff
{
    /*/
     * 
     *  - Properties ----------------------------------------
     *     MainDirectoryPath                              - Default main directory path (great path for saving data)
     *     
     *  - Methods ------------------------------------------
     *     GeneratePathFile()                             - Generates a path to a file
     *     GeneratePathDirectory(nameSubDir)              - Generate a path to a directory with the default main directory
     *     GeneratePathDirectory(pathMainDir, nameSubDir) - Generate a path to a directory with a custom main directory
     *     
    /*/

    /// <summary>
    /// Static class with commands for generating a path to files and directories
    /// </summary>
    public static class PathGenerator
    {
        /// <summary>
        /// Returns the directory path of the application (great path for saving data)
        /// </summary>
        public static string MainDirectoryPath => Application.persistentDataPath;

        /// <summary>
        /// Generates a path to a saveable file
        /// </summary>
        /// <param name="directoryPath">Parent directory path of the saveable file </param>
        /// <param name="prefix">Prefix of saveable file</param>
        /// <param name="name">Name of saveable file</param>
        /// <param name="fileType">File type of saveable file</param>
        /// <returns></returns>
        public static string GeneratePathFile(string directoryPath, string prefix, string name, string fileType)
            => $"{directoryPath}{prefix}-{name}.{fileType}";

        /// <summary>
        /// Extracts the file name from a file path
        /// </summary>
        /// <param name="directoryPath">Parent directory path of the saveable file </param>
        /// <param name="prefix">Prefix of file</param>
        /// <param name="path">Path of file</param>
        /// <param name="fileType">File type file</param>
        /// <returns></returns>
        public static string ExtractFileNameFromPath(string directoryPath, string prefix, string fileType, string path)
        {
            var removeFirst = $"{directoryPath}{prefix}-";
            if (!path.Contains(removeFirst))
                return null;
            var after = path.Remove(0, removeFirst.Length);

            if (!path.Contains(prefix)) 
                return null;
            var prefixCount = fileType.Length + 1;
            var startIndex = after.Length - prefixCount;
            return after.Remove(startIndex);
        }

        /// <summary>
        /// Generates a folder path inside the default main directory - <see cref="MainDirectoryPath"/>
        /// </summary>
        /// <param name="nameSubDir">Name of directory</param>
        /// <returns></returns>
        public static string GeneratePathDirectory(string nameSubDir) =>
            GeneratePathDirectory(MainDirectoryPath, nameSubDir);

        /// <summary>
        /// Generates a folder path at a specific parent folder, if you don't want to use the default main folder
        /// </summary>
        /// <param name="pathMainDir">Path of the custom main folder</param>
        /// <param name="nameSubDir">Name of the folder</param>
        /// <returns></returns>
        public static string GeneratePathDirectory(string pathMainDir, string nameSubDir)
            => $"{pathMainDir}\\{nameSubDir}\\";
    }
}
