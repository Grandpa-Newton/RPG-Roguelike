using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.UI;
using Unity.VisualScripting;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    [SerializeField] private UIInventoryItem item;

    private void Awake()
    {
        canvas = transform.parent.GetComponent<Canvas>();
        item = GetComponentInChildren<UIInventoryItem>();
    }


    public void SetData(Sprite sprite, int quantity)
    {
        item.SetData(sprite, quantity);
    }

    private void Update()
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform,
            Input.mousePosition,
            canvas.worldCamera,
            out position);

        transform.position = canvas.transform.TransformPoint(position);
    }

    public void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}