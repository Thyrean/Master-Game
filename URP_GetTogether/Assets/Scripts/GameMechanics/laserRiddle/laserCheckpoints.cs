using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class laserCheckpoints : MonoBehaviour
{
    public bool checkpointReached;

    public GameObject newLasersParent;
    public GameObject deleteLasersParent;
    public GameObject nextCP;

    public Component[] newLasers;
    public Component[] deleteLasers;

    private bool onePlayerReached;

    void Awake()
    {
        if(newLasersParent != null)
            newLasers = newLasersParent.GetComponentsInChildren<laserMovement>();

        if (deleteLasersParent != null)
            deleteLasers = deleteLasersParent.GetComponentsInChildren<laserMovement>();

        //ReactionManager.Add("CheckpointReached", CheckpointReached);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character"))
        {
            checkpointReached = true;

            for (var i = 0; i < newLasers.Length; i++)
            {
                newLasers[i].GetComponent<laserMovement>().resetPosition = gameObject.transform.position;
            }

            if (deleteLasers != null)
            {
                for (var i = 0; i < deleteLasers.Length; i++)
                {
                    deleteLasers[i].gameObject.SetActive(false);
                }
            }
            CheckpointReached();
        }
    }

    
    private void CheckpointReached()
    {
        if (checkpointReached == true && onePlayerReached == false)
        {
            ReactionManager.Call("CheckpointReached");
            onePlayerReached = true;
            Destroy(gameObject);

            if (nextCP != null)
                nextCP.SetActive(true);
        }
    }
    /*
    private void OnDestroy()
    {
        ReactionManager.Remove("CheckpointReached", CheckpointReached);
    }*/
}
