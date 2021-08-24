using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Lightbug.CharacterControllerPro.Core;

using Assets.Scripts.ActionReactionSystem;

public class distanceCollider : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject otherCollider;

    public GameObject lowerChar;
    public GameObject upperChar;

    public Transform upperResetPosition;
    public Transform lowerResetPosition;

    public GameObject localPlayer;
    public bool riddleStarted;



    public bool firstPlayerReached;
    public bool secondPlayerReached;

    // Start is called before the first frame update
    void Start()
    {
        characters = GameObject.FindGameObjectsWithTag("Character");

        riddleStarted = false;


        if (!NetworkServer.active)
            localPlayer = characters[1];

        if (NetworkServer.active)
            localPlayer = characters[0];

        /*if (!NetworkServer.active)
            Destroy(this);*/

        ReactionManager.Add("StartPointReached", StartPointReached);

        ReactionManager.Add("startDistanceRiddle", startDistanceRiddle);
        ReactionManager.Add("assign01", assign01);
        ReactionManager.Add("assign10", assign10);
    }

    // Update is called once per frame
    void Update()
    {
        //if(lowerChar != null && upperChar != null && riddleStarted == false)
          //  ReactionManager.Call("startDistanceRiddle");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == characters[0])
        {
            ReactionManager.Call("assign01");
        }

        else if (other.gameObject == characters[1])
        {
            ReactionManager.Call("assign10");
        }

        /*if (other.CompareTag("Character"))
        {
            ReactionManager.Call("startDistanceRiddle");
        }*/
    }

    public void OnTriggerExit(Collider other)
    {

        if (otherCollider != null)
        {
            if (other.gameObject == otherCollider.gameObject)
            {
                Debug.Log("Players failed");
                ResetRiddle();
            }
        }
    }

    private void startDistanceRiddle(string[] empty)
    {
        riddleStarted = true;

        ResetRiddle();

        otherCollider.transform.parent = upperChar.transform;
        otherCollider.transform.localPosition = Vector3.zero;
        //otherCollider.transform.position = upperChar.transform.position;// - new Vector3(0, 5, 0);
        otherCollider.GetComponent<Rigidbody>().isKinematic = true;

        gameObject.transform.parent = lowerChar.transform;
        gameObject.transform.localPosition = Vector3.zero;
        //gameObject.transform.position = lowerResetPosition.position;
    }

    private void assign01(string[] empty)
    {
        lowerChar = characters[0];
        upperChar = characters[1];
    }

    private void assign10(string[] empty)
    {
        lowerChar = characters[1];
        upperChar = characters[0];
    }

    private void ResetRiddle()
    {
        if (localPlayer == lowerChar)
            lowerChar.gameObject.GetComponent<CharacterActor>().Teleport(lowerResetPosition);

        if (localPlayer == upperChar)
            upperChar.gameObject.GetComponent<CharacterActor>().Teleport(upperResetPosition);
    }

    private void StartPointReached(string[] empty)
    {
        if (firstPlayerReached == true)
            secondPlayerReached = true;

        else if (firstPlayerReached == false)
            firstPlayerReached = true;

        if (firstPlayerReached == true && secondPlayerReached == true)
            ReactionManager.Call("startDistanceRiddle");
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("StartPointReached", StartPointReached);
        ReactionManager.Remove("startDistanceRiddle", startDistanceRiddle);
        ReactionManager.Remove("assign01", assign01);
        ReactionManager.Remove("assign10", assign10);
    }
}
