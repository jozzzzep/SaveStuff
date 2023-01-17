using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System;
using UnityEditor.PackageManager;

namespace SavesAPI.Advanced
{
    /*/ 
     * 
     * NOT RECOMMENDED TO USE DIRECTLY - Read documentation and examples here:
     *  https://github.com/jozzzzep/SavesAPI
     *  
     *  - Static Methods --------------------
     *     FileExists(...)                - Checks if a file exists in a path
     *     FileDelete(...)                - Deletes a file from a path only if it exists
     *     LoadDirectory<T>(...)          - Loads all saveable files from a chosen directory, if empty - list count will be 0
     *    --
     *     EncryptedSave<T>(...)          - Will save a saveable object to a file and encrypt it
     *     EncryptedLoad<T>(...)          - Will decrypt and load a saveable file
     *     EncryptedLoadDirectory<T>(...) - Will decrypt and load all saveable files in a chosen directory
     *    --
     *     JsonSave<T>(...)               - Will save a saveable object and serialize it to json format
     *     JsonLoad<T>(...)               - Will load a file and deserialize it from json to saveable type
     *     JsonLoadDirectory<T>(...)      - Will deserialize and load all saveable json files in a chosen directory
     *  -------------------------------------
     *  
    /*/

    /// <summary>
    /// Static commands of the SavesAPI, not recommended to use manually
    /// </summary>
    public static class StaticCommands
    {
        

        

      
    }
}
