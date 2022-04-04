using UnityEngine;

namespace Character
{
    /* This class exists because of Unity's hatred for static 
    fields and any action I attempt to perform with them. */
    public class AnimationAudios : MonoBehaviour
    {
        public void Footstep() => AudioManager.Footstep();
        public void BodyFall() => AudioManager.BodyFall();
        public void Thud() => AudioManager.Thud(); 
    }
}
