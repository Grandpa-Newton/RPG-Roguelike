using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryPage : MonoBehaviour
{
   [SerializeField] private UIInventoryItem itemPrefab;
   [SerializeField] private RectTransform contentPanel;

   private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

   public void InitializeInventoryUI(int inventorySize)
   {
      for (int i = 0; i < inventorySize; i++)
      {
         UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
         uiItem.transform.SetParent(contentPanel);
         listOfUIItems.Add(uiItem);
      }
   }

   public void Show()
   {
      gameObject.SetActive(true);
   }

   public void Hide()
   {
      gameObject.SetActive(false);
   }
}
