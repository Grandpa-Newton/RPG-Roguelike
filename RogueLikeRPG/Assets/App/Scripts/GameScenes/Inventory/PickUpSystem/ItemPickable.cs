using System.Collections;
using App.Scripts.DungeonScene.Items;
using App.Scripts.GameScenes.Weapon;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace App.Scripts.MixedScenes.PickUpSystem
{
   public enum ItemType
   {
      Coin,
      Food,
      
   }
   public class ItemPickable : MonoBehaviour
   {
      [field: SerializeField] public ItemSO InventoryItem { get; private set; }
      [field: SerializeField] public int Quantity { get; set; } = 1;
      [field: SerializeField] public ItemType ItemType { get; private set; }
      [SerializeField] private AudioClip audioSourceOnPick; 
      private AudioSource _audioSource;
      [SerializeField] private float duration = 0.3f;

      private void Awake()
      {
         _audioSource = GetComponent<AudioSource>();
      
      }

      // КАРЯВАЯ РЕАЛИЗАЦИЯ ЧЕРЕЗ 2 МЕТОДА, НО У МЕНЯ УЖЕ ГОЛОВА НЕ ВАРИТ, ПОТОМ ИЗМЕНИТЬ
      public void InitializeWeapon(WeaponItemSO item)
      {
         InventoryItem = item;
         Light2D inventoryItemLight = GetComponent<Light2D>();
         //InventoryItem.SetItemLight();
         inventoryItemLight.color = InventoryItem.itemLight.color;
         inventoryItemLight.intensity = InventoryItem.itemLight.intensity;
         inventoryItemLight.pointLightInnerRadius = InventoryItem.itemLight.pointLightInnerRadius;
         inventoryItemLight.pointLightOuterRadius = InventoryItem.itemLight.pointLightOuterRadius;
         inventoryItemLight.falloffIntensity = InventoryItem.itemLight.falloffIntensity;
      
         GetComponent<SpriteRenderer>().sprite = item.ItemImage;
      }
      public void InitializeItem()
      {
         Light2D light2D = GetComponent<Light2D>();
         light2D.color = this.InventoryItem.itemLight.color;
         light2D.intensity = this.InventoryItem.itemLight.intensity;
         light2D.pointLightInnerRadius = this.InventoryItem.itemLight.pointLightInnerRadius;
         light2D.pointLightOuterRadius = this.InventoryItem.itemLight.pointLightOuterRadius;
         light2D.falloffIntensity = this.InventoryItem.itemLight.falloffIntensity;
         Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
         GetComponent<SpriteRenderer>().sprite = this.InventoryItem.ItemImage;
      }

      public void DestroyItem()
      {
         GetComponent<Collider2D>().enabled = false;
         StartCoroutine(AnimateItemPickup());
      }
      private IEnumerator AnimateItemPickup()
      {
         _audioSource.clip = audioSourceOnPick;
         _audioSource.Play();
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
}
