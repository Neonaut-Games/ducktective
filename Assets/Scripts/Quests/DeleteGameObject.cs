using UnityEngine;

namespace Quests
{
    public class DeleteGameObject : MonoBehaviour
    {
        public GameObject objectToDestroy;
        private void OnEnable() => Destroy(objectToDestroy);
    }
}
