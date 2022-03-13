using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public String destination;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(destination);
    }
}
