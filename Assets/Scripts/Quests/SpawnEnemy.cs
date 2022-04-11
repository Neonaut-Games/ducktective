using UnityEngine;

namespace Quests
{
    public class SpawnEnemy : MonoBehaviour
    {
        public GameObject enemy;

        private void OnEnable()
        {
            Instantiate(enemy, new Vector3(0, 6, 0), Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
