using System;
using UnityEngine;

namespace UI
{
    public class Minimap : MonoBehaviour
    {
        public Transform target;
        private void FixedUpdate()
        {
            transform.position = new Vector3(target.position.x, transform.position.y, target.position.z);
        }
        
    }
}
