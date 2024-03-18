using Inventory;
using Inventory.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private const string NEXT_SCENE_TO_LOAD = "FirstLevelMap_02";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            /*var player = MapPlayerController.Instance.gameObject; // ��������
            Destroy(player);*/
            // GameObject.FindObjectOfType<InventoryController>().UnSubscribeEvent();
            SceneManager.LoadScene(NEXT_SCENE_TO_LOAD);
        }
    }
}
