using UnityEngine;

namespace Character.Player
{
    [RequireComponent(typeof(Material))]
    public class AttackIndicator : MonoBehaviour
    {

        public Color success = Color.green;
        public Color fail = Color.red;
        
        private MeshRenderer[] _renderers;
        private PlayerTPController _playerTp;

        private void Awake()
        {
            _renderers = GetComponentsInChildren<MeshRenderer>();
            _playerTp = FindObjectOfType<PlayerTPController>();
        }

        private void Update()
        {
            // Deal damage at the transform point
            Collider[] results = Physics.OverlapSphere(_playerTp.damagePoint.position, _playerTp.attackRange, _playerTp.damageableLayer.value);
            if (results.Length > 0)
            {
                foreach (var _renderer in _renderers) _renderer.material.color = success;
            }
            else
            {
                foreach (var _renderer in _renderers) _renderer.material.color = fail;
            }
            
        }
    }
}
