using UnityEngine;

namespace App.Scripts.AllScenes.UI
{
    public class StartShowLoadPanel : MonoBehaviour
    {
        void Start()
        {
            gameObject.SetActive(true);
        }
        public void HideLoadPanel()
        {
            gameObject.SetActive(false);
        }
    }
}
