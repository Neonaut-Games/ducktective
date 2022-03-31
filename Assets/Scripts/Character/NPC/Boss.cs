using System.Collections;
using Character.Player;
using Player;
using UnityEngine;

namespace Character.NPC
{
    public class Boss : Mortal
    {
    
        public int sightRange = 30;
        public GameObject victoryUI;

        public void Update()
        {
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            var bossPosition = rigidbody.position;

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
            StartCoroutine(ShowVictoryScreen());
        }

        private IEnumerator ShowVictoryScreen()
        {
            yield return new WaitForSeconds(4.0f);
            victoryUI.SetActive(true);
        }
    }
}
