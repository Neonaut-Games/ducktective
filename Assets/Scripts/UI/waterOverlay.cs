using UnityEngine;

public class WaterOverlay : MonoBehaviour
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
