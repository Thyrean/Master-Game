using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class laserRiddle : MonoBehaviour
{
    public GameObject nextLaserGate;

    public bool firstPlayerReached;
    public bool secondPlayerReached;

    // Start is called before the first frame update
    void Start()
    {
        ReactionManager.Add("CheckpointReached", CheckpointReached);
        ReactionManager.Add("openLaserGate", OpenLaserGate);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (firstPlayerReached == true && secondPlayerReached == true)
            ReactionManager.Call("openLaserGate");*/
    }

    private void OpenLaserGate(string[] empty)
    {
        if(firstPlayerReached == true && secondPlayerReached == true)
        {
            if (nextLaserGate != null)
                nextLaserGate.SetActive(true);

            Destroy(this.gameObject, 1f);
        }
    }

    private void CheckpointReached(string[] empty)
    {
        if (firstPlayerReached == true)
            secondPlayerReached = true;

        else if (firstPlayerReached == false)
            firstPlayerReached = true;

        if (firstPlayerReached == true && secondPlayerReached == true)
            ReactionManager.Call("openLaserGate");
    }

    private void OnDestroy()
    { 
        ReactionManager.Remove("CheckpointReached", CheckpointReached);
        ReactionManager.Remove("openLaserGate", OpenLaserGate);
    }
}
