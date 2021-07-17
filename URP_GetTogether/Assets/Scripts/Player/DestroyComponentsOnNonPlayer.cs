using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class DestroyComponentsOnNonPlayer : MonoBehaviour
    {
        [SerializeField]
        private Component[] componentsToKeep;

        [SerializeField]
        private Transform[] childrenToClear;

        [SerializeField]
        private GameObject[] childrenToDestroy;

        private void Start()
        {
            var localPlayer = PlayerId.LocalPlayer;

            if (localPlayer?.gameObject == gameObject)
                return;

            var toDestroy = new List<UnityEngine.Object>();

            var components = gameObject.GetComponents<Component>();
            foreach (var component in components)
            {
                if (component is Transform) continue;
                if (componentsToKeep.Contains(component))
                    continue;

                toDestroy.Add(component);
            }

            foreach (var child in childrenToClear)
            {
                foreach (var component in child.GetComponents<Component>())
                    toDestroy.Add(component);
            }

            foreach (var child in childrenToDestroy)
                toDestroy.Add(child);

            StartCoroutine(RemoveObjects(toDestroy));
        }


        private IEnumerator RemoveObjects(List<UnityEngine.Object> toDestroy)
        {
            while(toDestroy.Count > 0)
            {
                for (int i = 0; i < toDestroy.Count; i++)
                {
                    UnityEngine.Object obj = toDestroy[i];
                    if (obj is null)
                    {
                        toDestroy.RemoveAt(i);
                        if (i > 0) i--;
                        continue; 
                    }
                    if ((obj is Transform))
                    {
                        toDestroy.RemoveAt(i);
                        if (i > 0) i--;
                        continue; 
                    }

                    DestroyImmediate(obj);
                    if(obj == null)
                    {
                        toDestroy.RemoveAt(i);
                        if (i > 0) i--;
                    }
                }

                yield return null;
            }
        }
    }
}
