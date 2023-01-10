using UnityEngine;
using TMPro;
using SavesAPI;

namespace Examples.E02
{
    public class E02Script : MonoBehaviour
    {
        [SerializeField] TMP_InputField textField;

        E02SaveFile saveFile = new E02SaveFile("");

        private void Awake()
        {
            saveFile = ObjectsSaveSystem.InitializeObject(saveFile);
        }

        void Start()
        {

        }

        void Update()
        {

        }

        public void LoadData()
        {
            saveFile = ObjectsSaveSystem.Load(saveFile);
            textField.text = saveFile.textContent;
        }

        public void SaveData()
        {
            saveFile = new E02SaveFile(textField.text);
            ObjectsSaveSystem.Save(saveFile);
        }
    }
}
