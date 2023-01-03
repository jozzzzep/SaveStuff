using System;

namespace SavesAPI
{
    public interface ISaveable
    {
        /// <summary>
        /// The name of the savable Object
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns the last time the save has been created or modified
        /// </summary>
        public DateTime LastUsage { get; set; }
    }
}
