using UnityEngine;

namespace UI.Inspect.Lock
{
    public class LockTrigger : InspectTrigger
    {

        public static bool used = false;
        
        [Header("Lock Settings")]
        public string correctSequence;
        public GameObject reward;
        public bool shouldEnablePost = false;

        public void Awake()
        {
            if (!used) transform.SetPositionAndRotation(new Vector3(-109f, 3.5f, -1.35f), transform.rotation);
        }
        public override void Trigger()
        {
            DuckLog.Normal("Lock was triggered by " + gameObject.name);
            FindObjectOfType<LockManager>().StartLock(this);
        }
        
    }
}
