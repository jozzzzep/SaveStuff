using System.Linq;
using SavesAPI.Advanced;

namespace SavesAPI
{
    /// - Properties -----------
    ///     SlotsAmount        - Amount of slots in the slot based save system
    ///     FileType           - The file type of the saveable files
    ///     DirectoryPath      - The directory path the save system saves to and loads from
    ///     FilesPrefix        - The prefix of every file created with the save system
    /// - Methods --------------
    ///     Save(...)          - Saves an object to a file
    ///     Delete(...)        - Deletes a saved file
    ///     Load(...)          - Loads an object from a file
    ///     LoadIfExists(...)  - Loads a file only if it exits
    ///     LoadSlots()        - Loads all slots and returns the objects they store or a null array slot
    ///     SlotIsEmpty(...)   - 
    /// ------------------------

    /// <summary>
    /// A slot manager and sorter for a save system with fixed amount of slot
    /// </summary>
    /// <typeparam name="T">A saveable type class</typeparam>
    public class SlotSaveSystem<T> where T : SaveSlot
    {
        /// <summary>
        /// The internal save system, so you can choose which save system you want to has slot management
        /// </summary>
        private SaveSystem<T> internalSaveSystem;

        /// <summary>
        /// Amount of slots in save system
        /// </summary>
        public int SlotsAmount { get; private set; }

        /// <summary>
        /// File type of saveable files
        /// </summary>
        public string FileType => internalSaveSystem.FileType;

        /// <summary>
        /// The directory path the save system saves-to and loads-from
        /// </summary>
        public string DirectoryPath => internalSaveSystem.DirectoryPath;

        /// <summary>
        /// The prefix of every file created with the save system
        /// </summary>
        public string FilesPrefix => internalSaveSystem.FilesPrefix;

        /// <summary>
        /// A constructor for creating a slot based save system
        /// </summary>
        /// <param name="slotAmount"> Amount of slots in save system</param>
        /// <param name="internalSaveSystem">A sub-save-system for saving</param>
        public SlotSaveSystem(int slotAmount, SaveSystem<T> internalSaveSystem)
        {
            SlotsAmount = slotAmount;
            this.internalSaveSystem = internalSaveSystem;
        }

        public void Delete(int slot) => internalSaveSystem.Delete(SaveSlot.SlotToFileName(slot));
        public void Save(T toSave) => internalSaveSystem.Save(toSave.Name, toSave);
        public T Load(int slot) => internalSaveSystem.Load(SaveSlot.SlotToFileName(slot));
        public T LoadIfExists(int slot) => internalSaveSystem.LoadIfExists(SaveSlot.SlotToFileName(slot));

        public T[] LoadSlots()
        {
            var filesList = internalSaveSystem.LoadDirectory().ToList();
            var sorted = filesList.OrderBy(s => s.SlotIndex).ToArray();
            T[] slots = new T[SlotsAmount];
            for (int i = 0; i < sorted.Length; i++)
            {
                var slot = sorted[i];
                slots[slot.SlotIndex] = slot;
            }
            return slots;
        }
        
        public bool SlotIsEmpty(int slot) => !internalSaveSystem.FileExists(SaveSlot.SlotToFileName(slot));
        public bool FileSlotExists(int slot) => !SlotIsEmpty(slot);

        public static int? EmptySlotIndex(T[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
                if (slots[i] == null)
                    return i;
            return null;
        }
    }
}
