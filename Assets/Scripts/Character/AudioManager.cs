using UnityEngine;
using Random = UnityEngine.Random;

namespace Character
{
    public class AudioManager : MonoBehaviour
    {

        [Header("UI Audios")]
        public AudioSource pause;
        public AudioSource buttonClick;
        public AudioSource pop;
        public AudioSource decline;
        public AudioSource purchase;

        [Header("Miscellaneous Audios")]
        public AudioSource thud;
        public AudioSource impact;
        public AudioSource coin;
        
        [Header("Player-only Audios")]
        public AudioSource[] hurt;

        [Header("Boss-only Audios")]
        public AudioSource[] attackBoss;
        public AudioSource[] hurtBoss;
        public AudioSource[] stompBoss;
        
        [Header("Dialogue-related Audios")]
        public AudioSource messageSound;
        public AudioSource[] voicePlayer;
        public AudioSource[] voiceMom;
        public AudioSource[] voiceEddy;
        public AudioSource[] voiceRandy;
        public AudioSource[] voiceQuackintinius;
        public AudioSource[] voiceMeemaw;
        public AudioSource[] voiceBoss;
        
        [Header("Global Character Audios")]
        public AudioSource[] footstepSounds;
        public AudioSource bodyFall;

        public static AudioManager a;

        private void Start() => a = this;

        #region UI Audios

        public static void Pause() => a.pause.Play();
        public static void ButtonClick() => a.buttonClick.Play();

        public static void Pop() => a.pop.Play();
        public static void Decline() => a.decline.Play();
        public static void Purchase() => a.purchase.Play();
        
        #endregion

        #region Miscellaneous Audios

        public static void Thud() => a.thud.Play();

        public static void Impact() => a.impact.Play();

        public static void Coin() => a.coin.Play();

        #endregion
        
        #region Player-only Audios

        public static void Hurt() => PlayRandomSoundFromArray(a.hurt);

        #endregion

        #region Boss-only Audios

        public static void AttackBoss() => PlayRandomSoundFromArray(a.attackBoss);
        public static void HurtBoss() => PlayRandomSoundFromArray(a.hurtBoss);
        public static void StompBoss() => PlayRandomSoundFromArray(a.stompBoss);

        #endregion

        #region Dialogue-related Audios

        /* Most dialogue-related audio methods and accesses
        are found in the DialogueManager class. */

        #endregion

        #region Global Character Audios

        public static void Footstep() => PlayRandomSoundFromArray(a.footstepSounds);

        public static void BodyFall() => a.bodyFall.Play();

        #endregion
        
        private static void PlayRandomSoundFromArray(AudioSource[] audioSources)
        {
            audioSources[Random.Range(0, audioSources.Length)].Play();
        }
    }
}
