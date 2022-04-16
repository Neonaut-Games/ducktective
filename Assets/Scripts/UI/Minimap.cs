using UnityEngine;

namespace UI
{
    public class Minimap : MonoBehaviour
    {
        public Transform target;

        private void FixedUpdate()
        {
            var pos = transform.position;
            transform.position = new Vector3(target.position.x, pos.y, pos.z);
        }
    }
}
