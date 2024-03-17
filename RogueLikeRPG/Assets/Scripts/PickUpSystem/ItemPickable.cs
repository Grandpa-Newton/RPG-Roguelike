using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class ItemPickable : MonoBehaviour
{
   [field: SerializeField] public ItemSO InventoryItem { get; private set; }
   [field: SerializeField] public int Quantity { get; set; } = 1;
   [SerializeField] private AudioClip audioSourceOnPick; 
   private AudioSource audioSource;
   [SerializeField] private float duration = 0.3f;

   private void Awake()
   {
      audioSource = GetComponent<AudioSource>();
      
   }

   public void Initialize(ItemSO item)
   {
      
      InventoryItem = item;
      GetComponent<SpriteRenderer>().sprite = item.ItemImage;
   }

   public void DestroyItem()
   {
      GetComponent<Collider2D>().enabled = false;
      StartCoroutine(AnimateItemPickup());
   }
   private IEnumerator AnimateItemPickup()
   {
      audioSource.clip = audioSourceOnPick;
      audioSource.Play();
      Vector3 startScale = transform.localScale;
      Vector3 endScale = Vector3.zero;
      float currentTime = 0;

      while (currentTime < duration)
      {
         currentTime += Time.deltaTime;
         transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
         yield return null;
      }

      transform.localScale = endScale;
      Destroy(gameObject);
   }
}
