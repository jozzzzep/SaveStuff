using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SavesStuff;
using SavesStuff.Advanced;

namespace Examples.E01
{
    public class E01Script : MonoBehaviour
    {
        [SerializeField] TMP_InputField textField;

        EncryptedSaveSystem<E01SaveFile> saveSystem;

        private void Awake()
        {
            var dirPath = PathGenerator.GeneratePathDirectory("example1");
            saveSystem = new EncryptedSaveSystem<E01SaveFile>(dirPath, "save", "datata");
        }

        void Start()
        {

        }
        
        void Update()
        {

        }

        public void LoadData()
        {
            var loadedSave = saveSystem.Load(E01SaveFile.StaticName);
            textField.text = loadedSave.TextContent;
        }

        public void SaveData()
        {
            var newData = new E01SaveFile(textField.text);
            saveSystem.Save(newData);
        }
    }
}
