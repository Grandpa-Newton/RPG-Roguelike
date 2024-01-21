using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClicker : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D raycastHit;
            //RaycastHit raycastHit;

            float rayDistance = 100.0f;

            raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, rayDistance);

            if (raycastHit)
            {
                if(raycastHit.transform != null)
                {
                    Debug.Log(raycastHit.transform.gameObject);
                }
            }
        }
    }
}
