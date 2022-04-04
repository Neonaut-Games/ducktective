using System;
using System.Collections;
using Character.Player;
using UnityEngine;

namespace UI
{
    public class WaterOverlay : MonoBehaviour
    {
        private bool _isTakingDamage;
        
        private void OnTriggerStay(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (!_isTakingDamage) StartCoroutine(DamageTick());
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            StopAllCoroutines();
            _isTakingDamage = false;
        }

        private IEnumerator DamageTick()
        {
            _isTakingDamage = true;
            while (PlayerHealth.health > 0)
            {
                FindObjectOfType<PlayerHealth>().TakeDamage(10);
                yield return new WaitForSeconds(1.0f);
            }
        }
    }
}
