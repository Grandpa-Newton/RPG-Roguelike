using App.Scripts.AllScenes.Interfaces;
using App.Scripts.MixedScenes.Inventory.Controller;
using UnityEngine;

namespace App.Scripts.TraderScene
{
    public class PlayerInteractTraderController : MonoBehaviour // это, наверное, нужно будет вставить в playercontroller, и там тогда выключать playerinputactions, чтобы пользователь не мог бегац и стрелять
    {
        private IInteractable _traderInteractable;

        private GameObject _traderGameObject;

        [SerializeField] private InventoryController _inventoryController;
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.O))
            {
                _traderInteractable = GetInteractableObject();
                if (_traderInteractable != null)
                {
                    _inventoryController.SetTraderObject(_traderGameObject);
                    Debug.Log("Trader found!");
                }
                else
                {
                    _inventoryController.SetTraderObject(null);
                    Debug.Log("Trader not found!");
                }
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _traderInteractable = GetInteractableObject();
                if(_traderInteractable != null)
                {
                    _traderInteractable.Interact();
                }
            }
        }

        private IInteractable GetInteractableObject()
        {

            float interactRange = 2f;

            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, new Vector2(interactRange, interactRange), 0f);

            foreach (var collider in colliders)
            {
                if(collider.TryGetComponent(out IInteractable interactableObject))
                {
                    _traderGameObject = collider.gameObject;
                    return interactableObject;
                }
            }

            return null;
        }
    }
}
