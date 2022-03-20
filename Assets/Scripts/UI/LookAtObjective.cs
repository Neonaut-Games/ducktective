using Cinemachine;
using Player;
using UnityEngine;

namespace UI
{
    public class LookAtObjective : MonoBehaviour
    {
        
        public CinemachineBrain brainCamera;
        private int _currentQuestLevel;
        private GameObject _destination;
        
        private void Awake() => RefreshDestination();

        public void RefreshDestination()
        {
            _currentQuestLevel = PlayerLevel.GetLevel();
            _destination = GameObject.FindGameObjectWithTag("Objective" + _currentQuestLevel);
        }

        private void Update()
        {
            // Look towards the destination
            GameObject o;
            (o = gameObject).transform.LookAt(_destination.transform);
            
            // Position in front of the camera
        }
    }
}
