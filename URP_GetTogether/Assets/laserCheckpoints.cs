using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class laserCheckpoints : MonoBehaviour
{
    public bool checkpointReached;

    public GameObject laserParent;

    public Component[] lasers;
    private bool onePlayerReached;

    void Start()
    {
        if(laserParent != null)
            lasers = laserParent.GetComponentsInChildren<laserMovement>();

        //ReactionManager.Add("CheckpointReached", CheckpointReached);
    }

    // Update is called once per frame
    void Update()
    {
        if(checkpointReached == true && onePlayerReached == false)
        {
            ReactionManager.Call("CheckpointReached");
            onePlayerReached = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character"))
        {
            checkpointReached = true;
        }

        for (var i = 0; i < lasers.Length; i++)
        {
            lasers[i].GetComponent<laserMovement>().resetPosition = gameObject.transform.position;
        }
    }

    /*
    private void CheckpointReached(string[] empty)
    {
        
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("CheckpointReached", CheckpointReached);
    }*/
}
