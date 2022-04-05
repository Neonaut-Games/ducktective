using System.Collections;
using Character.Player;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

namespace Character.NPC
{
    public class Boss : Mortal
    {
    
        [Header("Combat Settings")]
        public int sightRange = 30;

        [Header("Reward Settings")]
        public bool shouldReward;
        [FormerlySerializedAs("victoryUI")] [CanBeNull] public GameObject rewardGameObject;

        public void Update()
        {
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            var bossPosition = mortalRigidbody.position;


            if (Vector3.Distance(playerPosition, bossPosition) > sightRange)
            {
                animator.SetBool("isMoving", false);
            }
            else
            {
                animator.SetBool("isMoving", true);
            }
        }

        public override void DeathReward()
        {
            if (shouldReward && rewardGameObject != null) StartCoroutine(ShowVictoryScreen());
        }

        private IEnumerator ShowVictoryScreen()
        {
            yield return new WaitForSeconds(4.0f);
            rewardGameObject!.SetActive(true);
        }
    }
}
