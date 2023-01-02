using System.Linq;

namespace SavesAPI
{
    public class SlotSaveSystem<T> where T : SaveSlot
    {
        public int SlotsAmount { get; private set; }
        public string FileType => internalSaveSystem.FileType;

        private SaveSystem<T> internalSaveSystem;

        public SlotSaveSystem(int slotAmount, SaveSystem<T> internalSaveSystem)
        {
            SlotsAmount = slotAmount;
            this.internalSaveSystem = internalSaveSystem;
        }

        public void Delete(int slot) => internalSaveSystem.Delete(SlotToFileName(slot));
        public void Save(int slot, T toSave) => internalSaveSystem.Save(SlotToFileName(slot), toSave);
        public T Load(int slot) => internalSaveSystem.Load(SlotToFileName(slot));
        public T LoadIfExists(int slot) => internalSaveSystem.LoadIfExists(SlotToFileName(slot));

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
        
        public bool SlotIsEmpty(int slot) => !internalSaveSystem.FileExists(SlotToFileName(slot));
        public bool FileSlotExists(int slot) => !SlotIsEmpty(slot);

        public string SlotToFileName(int slot) => "slot_" + slot.ToString();

        public static int? EmptySlotIndex(T[] slots)
        {
            for (int i = 0; i < slots.Length; i++)
                if (slots[i] == null)
                    return i;
            return null;
        }
    }
}
