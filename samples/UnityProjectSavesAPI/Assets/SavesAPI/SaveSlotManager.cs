using System;
using System.Linq;
using SavesAPI.Advanced;

namespace SavesAPI
{
    /*/
     * 
     *  - Properties ---------------
     *      SlotsAmount          - Max amount of slots in the slot based save system
     *      FileType             - The file type of the saveable files
     *      DirectoryPath        - The directory path the save system saves to and loads from
     *      FilesPrefix          - The prefix of every file created with the save system
     *      
     *  - Methods ------------------
     *      Save(...)            - Saves an object to a save slot
     *      Delete(...)          - Deletes a saved slot
     *      Load(...)            - Loads a saveable object from a save slot
     *      LoadIfExists(...)    - Loads a save slot only if it constains data
     *      LoadSlots()          - Loads all slots and returns an array with an object they store or a null in empty slots
     *      SlotIsEmpty(...)     - Checks if a slot is currently not storing data
     *      
     *  - Static Methods -----------
     *      EmptySlotIndex(...)  - Returns the first empty slot index, or null if there is none
     *      IndexToFileName(...) - Takes a slot index and returns a file-name
     *      
    /*/

    /// <summary>
    /// A slot manager and sorter for a save system with fixed amount of slot
    /// </summary>
    /// <typeparam name="T">A <see cref="SaveSlot"/> type class</typeparam>
    public class SaveSlotManager<T> : ManagedSaveSystem<T> where T : SaveSlot
    {
        /// <summary>
        /// Recent loaded slots
        /// </summary>
        public T[] LoadedSlots { get; private set; }

        /// <summary>
        /// Max amount of slots in save system
        /// </summary>
        public int SlotsAmount { get; private set; }

        public event Action<T[]> SlotsLoaded;

        /// <summary>
        /// A constructor for creating a slot based save system <br></br>
        /// Loads slots automatically when allocated
        /// </summary>
        /// <param name="slotAmount"> Amount of slots in save system</param>
        /// <param name="internalSaveSystem">A sub-save-system for saving</param>
        public SaveSlotManager(int slotAmount, SaveSystem<T> internalSaveSystem)
            : base(internalSaveSystem)
        {
            SlotsAmount = slotAmount;
            LoadSlots();
        }

        /// <summary>
        /// Deletes a saved slot
        /// </summary>
        /// <param name="slot">Slot index</param>
        public void Delete(int slot) => internalSaveSystem.Delete(IndexToFileName(slot));

        /// <summary>
        /// Saves an object to a save slot
        /// </summary>
        /// <param name="toSave">Object to save</param>
        /// 
        public void Save(T toSave) => internalSaveSystem.Save(toSave);

        /// <summary>
        /// Loads a saveable object from a save slot
        /// </summary>
        /// <param name="slot">Slot index</param>
        /// <returns></returns>
        public T Load(int slot) => internalSaveSystem.Load(IndexToFileName(slot));

        /// <summary>
        /// Loads a save slot only if it constains data
        /// </summary>
        /// <param name="slot">Slot index</param>
        /// <returns></returns>
        public T LoadIfExists(int slot) => internalSaveSystem.LoadIfExists(IndexToFileName(slot));

        /// <summary>
        /// Loads all slots and returns an array with the objects they store or a null in empty slots
        /// </summary>
        /// <returns></returns>
        public T[] LoadSlots()
        {
            var filesList = internalSaveSystem.GetAllFilePaths().ToList();
            var sorted = filesList.OrderBy(s => s.SlotIndex).ToArray();
            T[] slots = new T[SlotsAmount];
            for (int i = 0; i < sorted.Length; i++)
            {
                var slot = sorted[i];
                slots[slot.SlotIndex] = slot;
            }
            SlotsLoaded.SafeInvoke(LoadedSlots = slots);
            return LoadedSlots;
        }
        
        /// <summary>
        /// Checks if a slot is currently not storing data
        /// </summary>
        /// <param name="slot">Slot index</param>
        /// <returns></returns>
        public bool SlotIsEmpty(int slot) => !internalSaveSystem.FileExists(IndexToFileName(slot));

        /// <summary>
        /// When given array of slots:
        /// </summary>
        /// <param name="slots"></param>
        /// <returns>The first empty slot index, or null if there is not an empty slot</returns>
        public int? EmptySlotIndex() => EmptySlotIndex(LoadedSlots);

        /// <summary>
        /// Get value from a chosen loaded slot
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public T GetSlotValue(int slot) => LoadedSlots[slot];

        /// <summary>
        /// When given array of slots:
        /// </summary>
        /// <param name="slots"></param>
        /// <returns>The first empty slot index, or null if there is not an empty slot</returns>
        public static int? EmptySlotIndex<Type>(Type[] slots) where Type : class
        {
            for (int i = 0; i < slots.Length; i++)
                if (slots[i] == null)
                    return i;
            return null;
        }

        /// <summary>
        /// Takes a slot index and returns a file-name
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public static string IndexToFileName(int slot) => "slot_" + (slot + 1).ToString();
    }
}
