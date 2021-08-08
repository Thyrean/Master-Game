using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.DistanceRiddle
{
    [System.Serializable]
    public class DistancePair
    {
        [SerializeField]
        private Transform _player;

        [SerializeField]
        private Transform _relativeTransform;

        public void DrawDebug(Color color)
        {
            Debug.DrawLine(_player.position, _relativeTransform.position, color);
        }

        public DistancePair(Transform player, Transform relative)
        {
            _player = player;
            _relativeTransform = relative;
        }

        public Vector3 localPosition
        {
            get
            {
                //return _relativeTransform.InverseTransformPoint(_player.position);
                //return (_relativeTransform.position - _player.position).normalized * Vector3.Distance(_relativeTransform.position, _player.position)

                var relativeVector = _relativeTransform.InverseTransformPoint(_player.position);
                var distance = Vector3.Distance(_player.position, _relativeTransform.position);
                var magnifiedVector = Vector3.Scale(relativeVector, new Vector3(distance, 0, distance));
                return magnifiedVector;
            }
        }

        public static (DistancePair, DistancePair) GetPairs()
        {
            var players = GameObject.FindObjectsOfType<PlayerId>();
            var relativeTransforms = GameObject.FindGameObjectsWithTag("RelativeCheckPointDistanceRiddle");

            if(players.Length != 2)
            {
                Debug.Log("Found more or less then 2 Players !");
                return (null, null);
            }
            if(relativeTransforms.Length != 2)
            {
                Debug.Log("Found more or less then 2 RelativeCheckPointDistanceRiddle Objects !");
                return (null, null);
            }

            var playerA = players[0].transform;
            var playerB = players[1].transform;
            var relativeA = relativeTransforms[0].transform;
            var relativeB = relativeTransforms[1].transform;

            var distanceAA = Vector3.Distance(playerA.position, relativeA.position);
            var distanceAB = Vector3.Distance(playerA.position, relativeB.position);
            
            if(distanceAA < distanceAB)
            {
                var pairA = new DistancePair(playerA, relativeA);
                var pairB = new DistancePair(playerB, relativeB);
                return (pairA, pairB);
            }
            else
            {
                var pairA = new DistancePair(playerA, relativeB);
                var pairB = new DistancePair(playerB, relativeA);
                return (pairA, pairB);
            }

        }
    }
}
