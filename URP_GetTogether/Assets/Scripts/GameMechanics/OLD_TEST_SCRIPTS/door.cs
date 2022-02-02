using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;
using Mirror;
using UnityEngine;

public class door : MonoBehaviour    
{
    // Start is called before the first frame update
    void Start()
    {
        ReactionManager.Add("OpenDoor", OpenDoor);
    }

    private void OpenDoor(string[] parameters)
    {
        transform.Rotate(Vector3.up * 90);
    }
}
