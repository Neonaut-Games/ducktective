using Character;
using UnityEngine;

namespace UI.Inspect
{
    [RequireComponent(typeof(Animator))]
    public class InspectIcon : MonoBehaviour
    {
        private static InspectIcon _instance;
        public Animator icon;
        
        private static readonly int IsEnabled = Animator.StringToHash("isEnabled");

        private void Start() => _instance = this;

        public static void Enable()
        {
            DuckLog.Normal("The inspect indicator was enabled.");
            
            _instance.icon.SetBool(IsEnabled, true);
            AudioManager.Pop();
        }
        
        public static void Disable()
        {
            DuckLog.Normal("The inspect indicator was disabled.");
            
            _instance.icon.SetBool(IsEnabled, false);
            //AudioManager.Pop();
        }
        
    }
}