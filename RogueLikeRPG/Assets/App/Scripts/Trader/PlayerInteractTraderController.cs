using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractTraderController : MonoBehaviour // это, наверное, нужно будет вставить в playercontroller, и там тогда выключать playerinputactions, чтобы пользователь не мог бегац и стрелять
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            IInteractable interactable = GetInteractableObject();
            if(interactable != null)
            {
                interactable.Interact();
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
                return interactableObject;
            }
        }

        return null;
    }
}
