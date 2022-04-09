using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
    public class PersistentObject : MonoBehaviour
    {
        private static readonly Dictionary<string, PersistentObject> PersistentObjects =
            new Dictionary<string, PersistentObject>();

        void Awake()
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
    
    }
}
