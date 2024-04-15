using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.MixedScenes.Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        public void AddButton(string buttonName, Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = buttonName;
        }

        internal void Toggle(bool val)
        {
            if (val)
                RemoveOldButtons();
            
            gameObject.SetActive(val);
        }

        private void RemoveOldButtons()
        {
            foreach (Transform transformChildObjects in transform)
            {
                Destroy(transformChildObjects.gameObject);
            }
        }
    }
}