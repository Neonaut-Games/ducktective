using UnityEngine;

namespace UI
{
    /* Slowly rotates whatever object it is applied to
    along it's y-axis by a set number of degrees per tick. */
    public class Rotate : MonoBehaviour
    {
        public float angle;
        private Vector3 _axis;

        private void Start() => _axis = new Vector3(0, angle, 0);
        private void FixedUpdate() => transform.Rotate(_axis, Space.World);
    }
}
