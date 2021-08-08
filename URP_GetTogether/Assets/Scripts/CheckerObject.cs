using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    public class CheckerObject : MonoBehaviour
    {
        public bool IsEntered { get; private set; }
        public bool IsToggled { get; private set; } = false;

        public string targetTag = "null";

        public bool entered = false;

        public static event Action triggerUpdate;

        private void OnTriggerEnter(Collider other)
        {
            if(targetTag != "null")
            {
                if (!other.CompareTag(targetTag))
                    return;
            }

            IsEntered = true;
            entered = true;
            triggerUpdate?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (targetTag != "null")
            {
                if (!other.CompareTag(targetTag))
                    return;
            }

            IsEntered = false;
            entered = false;
            IsToggled = !IsToggled;
            triggerUpdate?.Invoke();
        }
    }
}
