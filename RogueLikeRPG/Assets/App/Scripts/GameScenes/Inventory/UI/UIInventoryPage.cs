using System;
using System.Collections.Generic;
using App.Scripts.MixedScenes.Inventory.Controller;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace App.Scripts.MixedScenes.Inventory.UI
{
    public class UIInventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem itemPrefab;
        [SerializeField] private RectTransform contentPanel;

        [FormerlySerializedAs("itemDescription")] [SerializeField] private UIInventoryDescription itemInformation;
        [SerializeField] private MouseFollower mouseFollower;

        private List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

        private int currantlyDraggedItemIndex = -1;

        private CanvasGroup _canvasGroup;

        public event Action<int> OnDescriptionRequested;
        public event Action<int> OnItemActionRequested;
        public event Action<int> OnStartDragging;

        public event Action<int, int> OnSwapItems;

        [SerializeField] private ItemActionPanel actionPanel;

        private void Awake()
        {
            mouseFollower.Toggle(false);
            itemInformation.ResetDescription();
        }

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
                uiItem.transform.SetParent(contentPanel);
                listOfUIItems.Add(uiItem);
                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDrag;
                uiItem.OnRightMouseBtnClick += HandleShowItemActions;
            }
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnItemActionRequested?.Invoke(index);
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (listOfUIItems.Count > itemIndex)
            {
                listOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleItemSelection(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            OnDescriptionRequested?.Invoke(index);
        }


        private void HandleSwap(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1 || currantlyDraggedItemIndex == -1)
            {
                return;
            }

            OnSwapItems?.Invoke(currantlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItemUI);
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
        {
            int index = listOfUIItems.IndexOf(inventoryItemUI);
            if (index == -1)
                return;
            currantlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItemUI);
            OnStartDragging?.Invoke(index);
        }

        private void HandleEndDrag(UIInventoryItem inventoryItemUI)
        {
            ResetDraggedItem();
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollower.Toggle(true);
            mouseFollower.SetData(sprite, quantity);
        }

        private void ResetDraggedItem()
        {
            mouseFollower.Toggle(false);
            currantlyDraggedItemIndex = -1;
        }


        public void ResetSelection()
        {
            itemInformation.ResetDescription();
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            actionPanel.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            actionPanel.Toggle(true);
            actionPanel.transform.position = listOfUIItems[itemIndex].transform.position;
        }


        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in listOfUIItems)
            {
                item.Deselect();
            }

            actionPanel.Toggle(false);
        }

        public void Show()
        {
            _canvasGroup.DOFade(1, 0.5f);
            ResetSelection();
        }
        public void Hide()
        {
            actionPanel.Toggle(false);
            _canvasGroup.DOFade(0, 0.5f);
            ResetDraggedItem();
        }

        public void UpdateDescription(int itemIndex, Sprite itemImage, string itemName, string itemDescription)
        {
            itemInformation.SetDescription(itemImage, itemName, itemDescription);
            DeselectAllItems();
            listOfUIItems[itemIndex].Select();
        }

        private List<UIInventoryItem> UpdateInventoryStateBetweenScenes()
        {
            if(!contentPanel)
            {
                contentPanel = GameObject.Find("Content").GetComponent<RectTransform>();
            }

            listOfUIItems = new List<UIInventoryItem>();
    
            foreach (RectTransform child in contentPanel)
            {
                UIInventoryItem item = child.GetComponent<UIInventoryItem>();
                if (item != null)
                {
                    listOfUIItems.Add(item);
                }
            }

            return listOfUIItems;
        }
        
        public void ResetAllItems()
        {
           
            foreach (var item in UpdateInventoryStateBetweenScenes())
            {
                item.ResetData();
                item.Deselect();
            } 
        }
    }
}