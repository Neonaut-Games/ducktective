using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
    public class PersistentObject : MonoBehaviour
    {
        private static Dictionary<string, PersistentObject> _persistentObjects = new Dictionary<string, PersistentObject>();

        void Awake()
        {
            if (_persistentObjects.ContainsKey(gameObject.name))
            {
                Debug.Log("A previously existing version of " + gameObject.name + " was found; destroying new object.");

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No previously existing versions of " + gameObject.name + " were found; keeping conserved object.");

                _persistentObjects.Add(gameObject.name, this);
                DontDestroyOnLoad(gameObject);
            }

        }
    
    }
}
