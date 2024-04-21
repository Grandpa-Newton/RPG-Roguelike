using App.Scripts.MixedScenes.Player.Control;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts
{
    public class GameVictoryEnder : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _textMesh;
        private void Start()
        {
            
        }

        public void EndGame()
        {
            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.DOFade(1.0f, 1.0f);
            _textMesh.DOScale(2.0f, 4.0f).OnComplete((LoadMainMenu));
        }

        private void LoadMainMenu()
        {
            MapLoader.WasSpawned = false;
            MapLoader.CurrentCellId = null;
            SceneManager.LoadScene("MainMenuScene");
        }
    }
}