using System;

namespace SavesAPI.Advanced
{
    /// <summary>
    /// Events class for slot save system
    /// </summary>
    public class SlotSaveSystemEvents<T> where T : SaveSlot
    {
        /// <summary>
        /// Raised when saving or deleting slots
        /// </summary>
        public event Action FilesUpdated;

        /// <summary>
        /// Raised when a slot has been saved to
        /// </summary>
        public event Action<T> Saved;

        /// <summary>
        /// Raised when a slot has been loaded
        /// </summary>
        public event Action<T> Loaded;

        /// <summary>
        /// Raised after a slot is deleted
        /// </summary>
        public event Action<int> Deleted;

        internal void OnUpdated() => FilesUpdated.SafeInvoke();

        internal void OnLoaded(T obj) => Loaded.SafeInvoke(obj);

        internal void OnSaved(T obj)
        {
            Saved.SafeInvoke(obj);
            OnUpdated();
        }

        internal void OnDeleted(int i)
        {
            Deleted.SafeInvoke(i);
            OnUpdated();
        }
    }
}
