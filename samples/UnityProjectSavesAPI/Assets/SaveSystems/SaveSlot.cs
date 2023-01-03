namespace SavesAPI
{
    public class SaveSlot : ISaveable
    {
        public int SlotIndex { get; private set; }
        public string Name => SlotIndex.ToString();

        public SaveSlot(int slotNumber) =>
            SlotIndex = slotNumber;
    }
}
