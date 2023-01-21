using System.Linq;
using Mono.Cecil;
using SavesAPI.Advanced;

namespace SavesAPI
{
    /*/
     * 
     *  - Properties ----------------
     *  
     *      InternalSaveSystem      - The internal save system
     *      SlotsAmount             - Max amount of slots in save system
     *      FileType                - The file type of the saveable files
     *      DirectoryPath           - The directory path the save system saves to and loads from
     *      FilesPrefix             - The prefix of every file created with the save system
     *      Events                  - Events of the save system
     *      
     *  - Methods -------------------
     *  
     *      Save(...)               - Saves an object to a file
     *      Load(...)               - Loads an object from a file
     *      Delete(...)             - Deletes a saved file
     *      DeleteByPath(...)       - Deletes a saved file
     *                             
     *      LoadIfExists(...)       - Loads a file only if it exits
     *      LoadIfExistsByPath(...) - Loads a file only if it exits
     *      FileExists(...)         - Checks if there is an existing saved file with a chosen name
     *      
     *      GetAllFilePaths()       - Returns all file paths of files that have been saved by the save system
     *      LoadMultiple(...)       - Loads multiple saved files
     *      LoadAllFiles()          - Loads all files that have been saved by the save system
     *      
     *      GeneratePath(...)       - Generates a path for a file based on a given file-name
     *      ExtractName(...)        - Extracts the file name from a file path
     *      
    /*/

    /// <summary>
    /// A slot manager and sorter for a save system with fixed amount of slot
    /// </summary>
    /// <typeparam name="T">A <see cref="SaveSlot"/> type class</typeparam>
    public class SlotSaveSystem<T> : ManagedSaveSystem<T> where T : SaveSlot
    {

        /// <summary>
        /// Max amount of slots in save system
        /// </summary>
        public int SlotsAmount { get; private set; }

        /// <summary>
        /// A constructor for creating a slot based save system <br></br>
        /// Loads slots automatically when allocated
        /// </summary>
        /// <param name="slotAmount"> Amount of slots in save system</param>
        /// <param name="internalSaveSystem">A sub-save-system for saving</param>
        public SlotSaveSystem(int slotAmount, SaveSystem<T> internalSaveSystem)
            : base(internalSaveSystem)
        {
            SlotsAmount = slotAmount;
        }

        /// <summary>
        /// Saves an object to a save slot
        /// </summary>
        /// <param name="toSave">Object to save</param>
        /// 
        public void Save(T toSave) => InternalSaveSystem.Save(toSave);

        /// <summary>
        /// Loads a saveable object from a save slot
        /// </summary>
        /// <param name="slot">Slot index</param>
        /// <returns></returns>
        public T Load(int slot) => InternalSaveSystem.Load(IndexToFileName(slot));

        /// <summary>
        /// Deletes a saved slot
        /// </summary>
        /// <param name="slot">Slot index</param>
        public void Delete(int slot) => InternalSaveSystem.Delete(IndexToFileName(slot));

        /// <summary>
        /// Loads a save slot only if it constains data
        /// </summary>
        /// <param name="slot">Slot index</param>
        /// <returns></returns>
        public T LoadIfExists(int slot) => InternalSaveSystem.LoadIfExists(IndexToFileName(slot));

        /// <summary>
        /// Checks which slots are currently empty and which are not
        /// </summary>
        /// <returns>Slot state - true if the slot is in use, false if it's empty</returns>
        public bool[] GetSlotsState()
        {
            var slotsState = new bool[SlotsAmount];
            for (int i = 0; i < SlotsAmount; i++)
                slotsState[i] = !SlotIsEmpty(i);
            return slotsState;
        }

        /// <inheritdoc cref="SlotSaveSystem{T}.LoadSlots(bool[])" />
        public T[] LoadSlots() => LoadSlots(GetSlotsState());

        /// <summary>
        /// Loads all slots and returns an array with the objects they store or a null in empty slots
        /// </summary>
        /// <returns></returns>
        public T[] LoadSlots(bool[] slotStates)
        {
            T[] slots = new T[SlotsAmount];
            for (int i = 0; i < SlotsAmount; i++)
                slots[i] = (slotStates[i] == true) ? Load(i) : null;
            return slots;
        }
        
        /// <summary>
        /// Checks if a slot is currently not storing data
        /// </summary>
        /// <param name="slot">Slot index</param>
        /// <returns></returns>
        public bool SlotIsEmpty(int slot) => !InternalSaveSystem.FileExists(IndexToFileName(slot));

        /// <summary>
        /// When given array of slots:
        /// </summary>
        /// <param name="slots"></param>
        /// <returns>The first empty slot index, or null if there is not an empty slot</returns>
        public int? EmptySlotIndex() => EmptySlotIndex(GetSlotsState());

        /// <summary>
        /// When given array of slots:
        /// </summary>
        /// <param name="slots"></param>
        /// <returns>The first empty slot index, or null if there is not an empty slot</returns>
        public static int? EmptySlotIndex(bool[] slotsState)
        {
            for (int i = 0; i < slotsState.Length; i++)
                if (!slotsState[i])
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
