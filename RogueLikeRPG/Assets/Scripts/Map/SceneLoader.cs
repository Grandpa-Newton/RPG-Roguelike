using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private const string NEXT_SCENE_TO_LOAD = "LevelLoadingScreen";
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            /*var player = MapPlayerController.Instance.gameObject; // ��������
            Destroy(player);*/
            
            SceneManager.LoadScene(NEXT_SCENE_TO_LOAD);
        }
    }
}
