using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Uninteractable
{
    [RequireComponent(typeof(Animator))]
    public class LoadingScreen : MonoBehaviour
    {
        public static bool isLoading;
        private Animator _animator;

        private static readonly int Start1 = Animator.StringToHash("start");
        private static readonly int Stop = Animator.StringToHash("stop");
        private void Start() => _animator = GetComponent<Animator>();
        
        public void Load(string sceneName) => StartCoroutine(LoadScene(sceneName));

        private IEnumerator LoadScene(string sceneName)
        {
            isLoading = true;
            _animator.SetTrigger(Start1);
            
            yield return new WaitForSeconds(0.75f); 
            
            /* Begin loading the new scene and hold
            the function until it is finished loading. */ 
            var loadOperation = SceneManager.LoadSceneAsync(sceneName); 
            while (!loadOperation.isDone) yield return null;

            // Adjust minimaps in-case of indoors/outdoors change
            var minimaps = FindObjectsOfType<MinimapTextureManager>();
            foreach (var minimap in minimaps) minimap.Refresh(sceneName);
            
            // Mandatory 0.25f seconds of wait time for camera adjustment
            yield return new WaitForSeconds(0.25f); 
            
            _animator.SetTrigger(Stop);
            
            // Wait until animator is done for pausing to be allowed
            yield return new WaitForSeconds(1.0f); 
            isLoading = false;
        }
        
    }
}
