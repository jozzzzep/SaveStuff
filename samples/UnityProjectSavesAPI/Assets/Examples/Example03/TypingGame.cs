using SavesAPI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Examples.E03
{
    public class SaveSlotCard : MonoBehaviour
    {
        ClickerGame game;

        [SerializeField] GameObject saveButton, loadButton, deleteButton;
        [SerializeField] int slotIndex;

        private void Start()
        {
            game = FindObjectOfType<ClickerGame>();
        }

        public void SetState(bool saveInUse)
        {
            saveButton.SetActive(!saveInUse);
            loadButton.SetActive(saveInUse);
            deleteButton.SetActive(saveInUse);
        }

        public void Save() => game.Save(slotIndex);

        public void Load() => game.LoadFromSlot(slotIndex);

        public void Delete() 
        { 

        }
    }

    public class ClickerGame : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;

        SlotSaveSystem<SaveFile> slotSystem;

        void Start()
        {
            var directoryPath = PathGenerator.GeneratePathDirectory("saveSlots");
            var internalSaveSystem = new JsonSaveSystem<SaveFile>(directoryPath, "slot");
            slotSystem = new SlotSaveSystem<SaveFile>(3, internalSaveSystem);
        }

        void Update()
        {

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
            slotSystem.LoadSlots();
        }

        public void LoadFromSlot(int slot)
        {
            var load = slotSystem.Load(slot);
            inputField.text = load.textContent;
        }

        public void Error(string message) { Debug.LogError(message); }
    }
}
