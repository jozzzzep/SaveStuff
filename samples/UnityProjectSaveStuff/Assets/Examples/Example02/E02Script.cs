using UnityEngine;
using TMPro;
using SavesStuff;

namespace Examples.E02
{
    public class E02Script : MonoBehaviour
    {
        [SerializeField] TMP_InputField textField;

        E02SaveFile saveFile = new E02SaveFile("");

        private void Awake()
        {
            saveFile = QuickSaveSystem.InitializeObject(saveFile);
        }

        public void LoadData()
        {
            saveFile = QuickSaveSystem.Load(saveFile);
            textField.text = saveFile.textContent;
        }

        public void SaveData()
        {
            saveFile = new E02SaveFile(textField.text);
            QuickSaveSystem.Save(saveFile);
        }
    }
}
