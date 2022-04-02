using System.Collections;
using Character.Player;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Collider))]
    public class OutOfBounds : MonoBehaviour
    {
    
        public GameObject boundaryMenu;

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            boundaryMenu.SetActive(true);
            StartCoroutine(DamageTick());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            boundaryMenu.SetActive(false);
            StopAllCoroutines();
        }

        private IEnumerator DamageTick()
        {
            while (PlayerHealth.health > 0)
            {
                FindObjectOfType<PlayerHealth>().TakeDamage(10);
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
