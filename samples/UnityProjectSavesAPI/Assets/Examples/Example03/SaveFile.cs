using SavesAPI.Slots;
using System;

namespace Examples.E03
{
    [Serializable]
    public class SaveFile : SaveSlot
    {
        public string textContent;

        public SaveFile(int slotNumber, string textContent)
            : base(slotNumber)
        {
            this.textContent = textContent;
        }
    }
}
