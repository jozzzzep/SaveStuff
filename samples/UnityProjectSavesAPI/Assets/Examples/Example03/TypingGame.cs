using SavesAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples.E03
{
    [Serializable]
    public class E03SaveFile : SaveSlot
    {
        public string textContent;

        public E03SaveFile(int slotNumber, string textContent)
            : base(slotNumber)
        {
            this.textContent = textContent;
        }
    }

    public class ClickerGame : MonoBehaviour
    {
        SaveSlotManager<E03SaveFile> saveSystem;

        // Start is called before the first frame update
        void Start()
        {
            var internalSaveSystem = new EncryptedSaveSystem<E03SaveFile>()
            saveSystem = new SlotSaveSystem(3, EncryptedSaveSystem<E03SaveFile>)
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
