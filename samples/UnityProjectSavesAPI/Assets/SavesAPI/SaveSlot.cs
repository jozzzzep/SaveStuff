using System;

namespace SavesAPI
{
    /// - Properties ---
    ///     SlotIndex  - Index of the save slot inside a SlotSaveSystem
    ///     Name       - The name of the file based on the slot index
    ///     LastUsage  - The last time the save has been created or modified
    /// ----------------

    /// <summary>
    /// A base class for a saveable classes to use in <see cref="SlotSaveSystem{T}"/>
    /// <para>Make sure to add attribute <see cref="System.Serializable"/></para>
    /// </summary>
    [Serializable]
    public class SaveSlot : ISaveable
    {
        /// <summary>
        /// Index of the save slot inside a <see cref="SlotSaveSystem{T}"/>
        /// </summary>
        public int SlotIndex { get; private set; }

        /// <summary>
        /// The name of the file based on the slot index
        /// </summary>
        public string Name => SlotSaveSystem<SaveSlot>.IndexToFileName(SlotIndex);

        public DateTime LastUsage { get; set; }

        /// <summary>
        /// The base constructor for a save-slot type class
        /// </summary>
        /// <param name="slotNumber"></param>
        public SaveSlot(int slotNumber) =>
            SlotIndex = slotNumber;
    }
}
