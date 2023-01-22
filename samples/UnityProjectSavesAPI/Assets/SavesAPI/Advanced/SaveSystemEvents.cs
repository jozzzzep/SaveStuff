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
        /// Raised when saving or deleting
        /// </summary>
        public event Action FilesUpdated;

        /// <summary>
        /// Raised when an object is saved 
        /// </summary>
        public event Action<T> Saved;

        /// <summary>
        /// Raised after loading an object
        /// </summary>
        public event Action<T> Loaded;

        /// <summary>
        /// Raised after deletion of a saved file, returns the path
        /// </summary>
        public event Action<string> Deleted;


        internal void OnUpdated() => FilesUpdated.SafeInvoke();

        internal void OnLoaded(T loadedObj) => Loaded.SafeInvoke(loadedObj);

        internal void OnSaved(T savedObj)
        {
            Saved.SafeInvoke(savedObj);
            OnUpdated();
        }

        internal void OnDeleted(string deletedFileName)
        {
            Deleted.SafeInvoke(deletedFileName);
            OnUpdated();
        }

    }
}
