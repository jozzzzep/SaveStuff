using SavesAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public class ClickerGame : MonoBehaviour
    {
        SaveSlotManager<SaveFile> slotSystem;

        void Start()
        {
            var directoryPath = PathGenerator.GeneratePathDirectory("saveSlots");
            var internalSaveSystem = new JsonSaveSystem<SaveFile>(directoryPath, "slot");
            slotSystem = new SaveSlotManager<SaveFile>(3, internalSaveSystem);
        }

        void Update()
        {

        }

        public void Save()
        {
            int emptySlot = SaveSlotManager.EmptySlotIndex(loadedSlots);
        }

        public void Save(int slot)
        {

        }
    }
}
