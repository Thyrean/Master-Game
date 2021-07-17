using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        ReactionManager.Call("CapsuleInteraction");
    }
}
