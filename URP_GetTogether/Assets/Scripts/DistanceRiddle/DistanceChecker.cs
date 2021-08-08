using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DistanceRiddle
{
    [Serializable]
    public class DistanceChecker
    {
        [SerializeField]
        public DistancePair _playerA;

        [SerializeField]
        public DistancePair _playerB;

        //public float maxDistance;
        
        public float CurrentDistance 
        { 
            get
            {
                var positionA = _playerA.localPosition;
                var positionB = _playerB.localPosition; 

                if(positionA == null || positionB == null)
                {
                    Debug.LogWarning("Player Pairs not valid !");
                    return float.PositiveInfinity;
                }

                return Vector3.Distance(positionB, positionA);
            } 
        }

        bool initialized = false;
        public bool Valid { get => initialized; }

        public void Init()
        {
            var pairs = DistancePair.GetPairs();
            
            if(pairs.Item1 == null || pairs.Item2 == null)
            {
                Debug.Log("Invaliid pairs found !");
                return;
            }

            _playerA = pairs.Item1;
            _playerB = pairs.Item2;

            initialized = true;
        }

        public bool DistanceIsValid(float maxDistance)
        {
            return CurrentDistance <= maxDistance;
        }

        public void DebugDraw(float maxDistance)
        {
            var distanceIsValid = DistanceIsValid(maxDistance);
            var color = distanceIsValid ? Color.green : Color.red;

            _playerA.DrawDebug(color);
            _playerB.DrawDebug(color);
        }
    }
}
