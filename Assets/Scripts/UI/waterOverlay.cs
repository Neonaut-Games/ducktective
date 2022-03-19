using System.Collections;
using Player;
using UnityEngine;

namespace UI
{
    public class WaterOverlay : MonoBehaviour
    {
        public GameObject overlay;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            overlay.SetActive(true);
            StartCoroutine(DrownTick());
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            overlay.SetActive(false);
            StopAllCoroutines();
        }

        private IEnumerator DrownTick()
        {
            while (PlayerHealth.health > 0)
            {
                FindObjectOfType<PlayerHealth>().TakeDamage(10);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
