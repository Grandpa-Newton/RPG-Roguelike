using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.MixedScenes.Inventory.UI
{
    public class UIInventoryDescription : MonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;

        private void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription()
        {
            title.text = "";
            description.text = "";
        }

        public void SetDescription(Sprite sprite, string itemName, string itemDescription)
        {
            title.text = itemName;
            description.text = itemDescription;
        }
    
    }
}
