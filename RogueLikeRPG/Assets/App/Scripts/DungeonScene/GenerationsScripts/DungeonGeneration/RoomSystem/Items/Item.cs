using UnityEngine;

namespace App.Scripts.DungeonScene.GenerationsScripts.DungeonGeneration.RoomSystem.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BoxCollider2D itemCollider;

        [SerializeField] private int health = 3; 
        private bool _nonDestructible;
    
        [SerializeField]
        private GameObject hitFeedback, destoyFeedback;

    
    
        public void Initialize(ItemData itemData)
        {
            spriteRenderer.sprite = itemData.sprite;
            spriteRenderer.transform.localPosition = new Vector2(0.5f * itemData.size.x, 0.5f * itemData.size.y);
        
            itemCollider.size = new Vector2(itemData.colliderSize.x, itemData.colliderSize.y);
            itemCollider.offset = new Vector2(itemData.colliderOffset.x, itemData.colliderOffset.y);
            if (itemData.nonDestuctible)
                _nonDestructible = true;

            health = itemData.health;
        }
    
        public void GetHit(int damage, GameObject damageDealer)
        {
            if (_nonDestructible)
                return;
            if(health>1)
                Instantiate(hitFeedback, spriteRenderer.transform.position, Quaternion.identity);
            else
                Instantiate(destoyFeedback, spriteRenderer.transform.position, Quaternion.identity);
            //spriteRenderer.transform.DOShakePosition(0.2f, 0.3f, 75, 1, false, true).OnComplete(ReduceHealth);
        }
    
        private void ReduceHealth()
        {
            health--;
            if (health <= 0)
            {
                //spriteRenderer.transform.DOComplete();
                Destroy(gameObject);
            }
            
        }
    }
}
