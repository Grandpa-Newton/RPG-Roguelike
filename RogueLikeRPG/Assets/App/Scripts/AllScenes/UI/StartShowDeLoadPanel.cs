using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.Scripts.AllScenes.UI
{
    public class StartShowDeLoadPanel : MonoBehaviour
    {
        private Animator _animator;
        private NextLevelStarter _nextLevelStarter;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            //yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        }
 
        public void InstanceOnInteractPressed(string sceneToLoad)
        {
            MapInputManager.Instance.OnInteractPressed -= InstanceOnInteractPressed;
            Debug.Log("BABIZYANA");
            gameObject.SetActive(true);
            _animator.SetTrigger("StartAnim");
            StartCoroutine(ExitAfterAnimation(sceneToLoad));
        }

        private void Start()
        {
            MapInputManager.Instance.OnInteractPressed += InstanceOnInteractPressed;
            gameObject.SetActive(false);
        }

        IEnumerator ExitAfterAnimation(string sceneToLoad)
        {
            yield return new WaitWhile(() => _animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.0f);
            ShowPortalInteractButtonUI.SceneName = SceneManager.GetActiveScene().name;
            SceneLoader.NEXT_SCENE_TO_LOAD = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneToLoad);
        }

        /*IEnumerator ExitAfterAnimation(string sceneToLoad)
    {
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(sceneToLoad);
    }*/

        public void HideDeLoadPanel()
        {
            gameObject.SetActive(false);
        }
    }
}