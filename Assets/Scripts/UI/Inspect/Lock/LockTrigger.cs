using UnityEngine;

namespace UI.Inspect.Lock
{
    public class LockTrigger : InspectTrigger
    {
        
        [Header("Object Settings")]
        public GameObject lockUI;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            lockUI.SetActive(true);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            lockUI.SetActive(false);
        }
        
        public override void Trigger()
        {
            DuckLog.Normal("Lock was triggered by " + gameObject.name);
            FindObjectOfType<LockManager>().StartLock(this);
        }
        
    }
}
