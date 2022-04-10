using Character.Player;
using UnityEngine;

namespace Quests
{
    public class Heal : MonoBehaviour
    {
        private PlayerHealth _health;
        private void OnEnable()
        {
            if (_health == null) _health = FindObjectOfType<PlayerHealth>();
            _health.Heal(10);
            gameObject.SetActive(false);
        }
    }
}
