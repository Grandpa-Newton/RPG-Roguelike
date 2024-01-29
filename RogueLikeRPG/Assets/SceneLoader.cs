using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SceneLoader : MonoBehaviour
{
    private const string NEXT_SCENE_TO_LOAD = "FirstLevelMap_01";
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var player = MapPlayerController.Instance.gameObject; // »«Ã≈Õ»“‹
            Destroy(player);
            SceneManager.LoadScene(NEXT_SCENE_TO_LOAD);
        }
    }
}
