using System;

namespace SavesAPI
{
    /// - Properties ---
    ///     Name       - The name of the saveable Object
    ///     LastUsage  - The last time the save has been created or modified
    /// ----------------

    /// <summary>
    /// Interface for creating a saveable class for save systems
    /// </summary>
    public interface ISaveable
    {
        /// <summary>
        /// The name of the saveable Object
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Returns the last time the save has been created or modified
        /// </summary>
        public DateTime LastUsage { get; set; }
    }
}
