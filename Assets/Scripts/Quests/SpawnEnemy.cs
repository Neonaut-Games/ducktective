using System;
using UnityEngine;

namespace Quests
{
    public class SpawnEnemy : MonoBehaviour
    {
        public GameObject enemy;

        private void OnEnable()
        {
            Instantiate(enemy);
            gameObject.SetActive(false);
        }
    }
}
