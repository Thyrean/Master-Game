using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sceneObjectSpawnerDebug : MonoBehaviour
{
    public NetworkIdentity newSceneObject;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            if (NetworkServer.active)
            {
                StartCoroutine(WaitForClient(1.0f));
            }

            if (!NetworkServer.active)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Spawn()
    {
        if (newSceneObject == null)
            Debug.Log("No object to spawn");

        else
        {
            Debug.Log("Spawning");

            GameObject go = (GameObject)Instantiate(newSceneObject.gameObject, gameObject.transform.position, gameObject.transform.rotation);
            NetworkServer.Spawn(go);
        }
    }

    private IEnumerator WaitForClient(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Spawn();
    }
}
