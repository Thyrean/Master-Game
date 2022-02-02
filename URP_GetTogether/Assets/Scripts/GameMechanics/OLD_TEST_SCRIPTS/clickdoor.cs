using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;
using Mirror;
using UnityEngine;

public class clickdoor : MonoBehaviour
{
    private void OnMouseDown()
    {
        ReactionManager.Call("OpenDoor");
    }
}
