using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public class LoadingScreen : MonoBehaviour
    {
        private Animator _animator;
        private void Start() => _animator = GetComponent<Animator>();
        
        public void Load(string sceneName)
        {
            StartCoroutine(LoadScene(sceneName));
        }

        private IEnumerator LoadScene(string sceneName)
        {
           _animator.SetTrigger("start");
           yield return new WaitForSeconds(0.75f);
           /* Begin loading the new scene and hold
           the function until it is finished loading. */
           var loadOperation = SceneManager.LoadSceneAsync(sceneName);
           while (!loadOperation.isDone) yield return null; 
           // Mandatory 0.25f seconds of wait time for camera adjustment
           yield return new WaitForSeconds(0.25f);
           _animator.SetTrigger("stop");
        }
    }
}
