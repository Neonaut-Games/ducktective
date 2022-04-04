using UnityEngine;

namespace Character
{
    public class CharacterAudios : MonoBehaviour
    {
        public AudioSource[] footstepSounds;
        public AudioSource bodyFall;

        public void Footstep()
        {
            var sound = footstepSounds[Random.Range(0, footstepSounds.Length)];
            sound.Play();
        }

        public void BodyFall() => bodyFall.Play();
    }
}
