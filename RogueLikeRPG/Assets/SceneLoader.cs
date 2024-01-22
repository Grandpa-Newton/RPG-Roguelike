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
            SceneManager.LoadScene(NEXT_SCENE_TO_LOAD);
            GameObject.Find("MapLoader").GetComponent<MapLoader>().UpdateInfo();
        }
    }
}
