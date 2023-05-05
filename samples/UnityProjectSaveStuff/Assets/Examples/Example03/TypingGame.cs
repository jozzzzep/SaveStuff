using SavesStuff;
using TMPro;
using UnityEngine;

namespace Examples.E03
{
    public class TypingGame : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;
        [SerializeField] SaveSlotCard[] saveSlotCards;

        SlotSaveSystem<SaveFile> slotSystem;

        void Start()
        {
            var directoryPath = PathGenerator.GeneratePathDirectory("saveSlots");
            var internalSaveSystem = new JsonSaveSystem<SaveFile>(directoryPath, "slot");
            slotSystem = new SlotSaveSystem<SaveFile>(3, internalSaveSystem);

            slotSystem.Events.FilesUpdated += UpdateSlots;
            UpdateSlots();
        }

        void Update()
        {

        }

        public void UpdateSlots()
        {
            var slotsState = slotSystem.GetSlotsState();
            for (int i = 0; i < saveSlotCards.Length; i++)
                saveSlotCards[i].SetState(slotsState[i]);
        }

        public void Save()
        {
            var emptySlot = slotSystem.EmptySlotIndex();
            if (emptySlot != null)
                Save(emptySlot.Value);
            else
                Error("No empty slots available");
        }

        public void Save(int slot)
        {
            var save = new SaveFile(slot, inputField.text);
            slotSystem.Save(save);
        }

        public void Delete(int slot) =>
            slotSystem.Delete(slot);

        public void LoadFromSlot(int slot)
        {
            var load = slotSystem.Load(slot);
            inputField.text = load.textContent;
        }

        public void Error(string message) =>
            Debug.LogError(message);
    }
}
