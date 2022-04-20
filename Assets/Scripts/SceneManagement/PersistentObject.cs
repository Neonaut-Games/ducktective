using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class PersistentObject : MonoBehaviour
    {
        public static Dictionary<string, PersistentObject> PersistentObjects = 
            new Dictionary<string, PersistentObject>();

        private void Start()
        {
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }
        
        private void Awake()
        {
            if (PersistentObjects.ContainsKey(gameObject.name))
            {
                DuckLog.Normal(
                    "A previously existing version of "+ gameObject.name + " was found;" +
                    " destroying new object.");

                Destroy(gameObject);
            }
            else
            {
                DuckLog.Normal(
                    "No previously existing versions of " + gameObject.name + " were found;" +
                    " keeping conserved object.");

                PersistentObjects.Add(gameObject.name, this);
                DontDestroyOnLoad(gameObject);
            }

        }
        
        private void OnActiveSceneChanged(Scene current, Scene next)
        {
            var sceneName = SceneManager.GetActiveScene().name;
            if (!sceneName.ToLower().Contains("house05")) return;
            
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            PersistentObjects.Remove(gameObject.name);
        }
    
    }
}
