using System;
using SavesAPI.Advanced;

namespace SavesAPI.Slots
{
    public class SlotSaveSystemEvents<T> where T : SaveSlot
    {
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

        internal void OnSaved(T obj) => Saved.SafeInvoke(obj);
        internal void OnLoaded(T obj) => Loaded.SafeInvoke(obj);
        internal void OnDeleted(int i) => Deleted.SafeInvoke(i);
    }
}
