using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] Transform target;

    private void Update()
    {
        if(target == null)
        {
            target = FindTarget();
            return;
        }

        transform.LookAt(target);
        transform.Rotate(Vector3.up, 180);
    }

    private Transform FindTarget()
    {
        var obj = GameObject.FindGameObjectWithTag("CharCam");
        if (obj == null) return null;
        return obj.transform;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
