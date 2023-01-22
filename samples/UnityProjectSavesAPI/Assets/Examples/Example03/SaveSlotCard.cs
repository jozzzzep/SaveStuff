using TMPro;
using UnityEngine;

namespace Examples.E03
{
    public class SaveSlotCard : MonoBehaviour
    {
        TypingGame game;

        [SerializeField] GameObject saveButton, loadButton, deleteButton;
        [SerializeField] int slotIndex;

        private void Start()
        {
            game = FindObjectOfType<TypingGame>();
        }

        public void SetState(bool saveInUse)
        {
            Debug.Log(saveInUse);
            saveButton.SetActive(!saveInUse);
            loadButton.SetActive(saveInUse);
            deleteButton.SetActive(saveInUse);
        }

        public void Save() => game.Save(slotIndex);

        public void Load() => game.LoadFromSlot(slotIndex);

        public void Delete() => game.Delete(slotIndex);
    }
}
