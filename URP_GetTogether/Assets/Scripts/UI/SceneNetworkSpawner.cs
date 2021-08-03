using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNetworkSpawner : MonoBehaviour 
{
    public NetworkIdentity[] spawnPrefab;
    //System.Guid[] savedIDs;

    //public Guid spawnId;
    /*void Update()
     {
         if(Input.GetKeyDown(KeyCode.P))
             Spawn();
     }

    private void Spawn()
    {
        

        if (spawnPrefab == null)
            Debug.Log("No objects to spawn");

        //else if (spawnPrefabs != null){     NetworkServer.Spawn(spawnPrefabs[i].gameObject);}}
        
    else
        {
            Debug.Log("P pressed");
            for (var i = 0; i < spawnPrefab.Length; i++)
            {
                GameObject go = (GameObject)Instantiate(spawnPrefab[i].gameObject, transform.position, transform.rotation);
                NetworkServer.Spawn(go);
                go.transform.position = new Vector3(1, 1, 1);
            }
        }
    }*/

    private void Awake()
    {
        if (NetworkServer.active)
        {
            Debug.Log("Server Reaction");
            for (var i = 0; i < spawnPrefab.Length; i++)
            {
                spawnPrefab[i].assetId = System.Guid.NewGuid();

                NetworkServer.Spawn(spawnPrefab[i].gameObject, spawnPrefab[i].assetId);

                //GameObject spawnThis = (GameObject)Instantiate(spawnPrefabs[i].gameObject, transform.position, transform.rotation);
                //savedIDs[i] = spawnPrefab[i].assetId;
            }
        }

        if(!NetworkServer.active)
        {
            ClientScene.PrepareToSpawnSceneObjects();

            Debug.Log("Client Reaction");
            for (var i = 0; i < spawnPrefab.Length; i++)
            {
                Debug.Log("Client Looping");

                //spawnPrefab[i].assetId = System.Guid.NewGuid();

                //ClientScene.RegisterPrefab(spawnPrefab[i].gameObject, savedIDs[i]);
                
                //GameObject.Instantiate(spawnPrefab[i].gameObject, spawnPrefab[i].gameObject.transform.position, Quaternion.identity);
                ClientScene.RegisterSpawnHandler(spawnPrefab[i].assetId, Spawn, Unspawn);
            }
        }    
    }

    public GameObject Spawn(SpawnMessage msg)
    {
        return GameObject.Instantiate(spawnPrefab[0].gameObject, msg.position, msg.rotation);
    }

    private void Unspawn(GameObject go)
    {
        Destroy(go);
    }















    /*
     for (var i = 0; i < spawnPrefabs.Length; i++)
        {
            spawnId = spawnPrefabs[i].GetComponent<NetworkIdentity>().assetId;
            SpawnOnClient(spawnPrefabs[i].GetComponent<NetworkIdentity>().netId);
            
        }
    
    private void SpawnOnClient(uint itemID)
    {
        GameObject spawnObj = NetworkIdentity.spawned[itemID].gameObject;

        Instantiate(spawnObj.gameObject, transform.position, transform.rotation);
    }*/




    /*ClientScene.RegisterSpawnHandler(spawnId, spawnHandler: Spawn, Unspawn);

    */

    /*foreach(NetworkIdentity spawnThis in spawnPrefabs)
        {
            Instantiate(spawnThis.gameObject, transform.position, transform.rotation);
            NetworkServer.Spawn(spawnThis.gameObject);
        }*/
}
