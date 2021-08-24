using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class distanceEndPoint : MonoBehaviour
{
    public bool checkpointReached;
    private bool onePlayerReached;

    // Start is called before the first frame update
    void Update()
    {
        if (checkpointReached == true && onePlayerReached == false)
        {
            ReactionManager.Call("EndPointReached");
            onePlayerReached = true;
            //Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character"))
        {
            checkpointReached = true;
        }
    }
}
