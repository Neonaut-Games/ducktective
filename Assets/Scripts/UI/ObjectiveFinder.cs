using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectiveFinder : MonoBehaviour
{
    public GameObject objectiveFinder;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            objectiveFinder.GetComponent<Renderer>().enabled = false;
            objectiveFinder.SetActive(!objectiveFinder.activeSelf);
        }
    }
    
}
