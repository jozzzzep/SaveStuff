using System.Collections.Generic;
using System;

namespace SavesAPI.Advanced
{
    /// <summary>
    /// Events for save systems
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SaveSystemEvents<T> where T : class, ISaveable
    {
        /// <summary>
        /// Called when an object is saved 
        /// </summary>
        public event Action<T> Saved;

        /// <summary>
        /// Called after loading an object
        /// </summary>
        public event Action<T> Loaded;

        /// <summary>
        /// Called after deletion of a saved file, returns the path
        /// </summary>
        public event Action<string> Deleted;

        /// <summary>
        /// Called after directory loading
        /// </summary>
        public event Action<List<T>> DirectoryLoaded;

        public void OnSaved(T savedObj) => Saved.SafeInvoke(savedObj);
        public void OnLoaded(T loadedObj) => Loaded.SafeInvoke(loadedObj);
        public void OnDeleted(string deletedFileName) => Deleted.SafeInvoke(deletedFileName);
        public void OnDirectoryLoaded(List<T> loadedObjects) => DirectoryLoaded.SafeInvoke(loadedObjects);
    }
}
