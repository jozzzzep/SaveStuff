using System;

namespace SavesAPI
{
    public class SaveSlot : ISaveable
    {
        /// <summary>
        /// Index of save slot inside a <see cref="SlotSaveSystem{T}"/>
        /// </summary>
        public int SlotIndex { get; private set; }

        /// <summary>
        /// The name of the file is the save slot number
        /// </summary>
        public string Name => SlotToFileName(SlotIndex);

        public DateTime LastUsage { get; set; }

        public SaveSlot(int slotNumber) =>
            SlotIndex = slotNumber;

        public static string SlotToFileName(int slot) => "slot_" + (slot + 1).ToString();
    }
}
