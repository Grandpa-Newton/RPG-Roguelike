using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private const string NEXT_SCENE_TO_LOAD = "MapScene_01";

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
