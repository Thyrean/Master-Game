using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class distanceRiddleDestroy : MonoBehaviour
{
    public bool firstPlayerReached;
    public bool secondPlayerReached;

    public GameObject DistanceCollider1;
    public GameObject DistanceCollider2;

    public GameObject[] Walls;

    // Start is called before the first frame update
    void Start()
    {
        Walls = GameObject.FindGameObjectsWithTag("laserWall");

        ReactionManager.Add("EndPointReached", EndPointReached);
        ReactionManager.Add("DestroyDistanceRiddle", DestroyDistanceRiddle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void EndPointReached(string[] empty)
    {
        if (firstPlayerReached == true)
            secondPlayerReached = true;

        else if (firstPlayerReached == false)
            firstPlayerReached = true;

        if (firstPlayerReached == true && secondPlayerReached == true)
            ReactionManager.Call("DestroyDistanceRiddle");
    }

    private void DestroyDistanceRiddle(string[] empty)
    {
        for (var i = 0; i < Walls.Length; i++)
            Walls[i].SetActive(false);

        DistanceCollider1.transform.parent = null;
        DistanceCollider2.transform.parent = null;

        Destroy(DistanceCollider1.GetComponent<distanceCollider>());
        DistanceCollider2.SetActive(false);

        //Destroy(DistanceCollider1);
        //Destroy(DistanceCollider2);

        Destroy(this);
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("DestroyDistanceRiddle", DestroyDistanceRiddle);
        ReactionManager.Remove("EndPointReached", EndPointReached);
    }
}
