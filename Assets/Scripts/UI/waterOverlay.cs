using System;
using UnityEngine;

public class waterOverlay : MonoBehaviour
{
    public GameObject overlay;

    private void OnTriggerEnter(Collider other)
    {
        overlay.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        overlay.SetActive(false);
    }
}
