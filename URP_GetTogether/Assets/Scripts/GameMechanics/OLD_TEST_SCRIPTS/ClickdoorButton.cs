using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickdoorButton : MonoBehaviour
{
    [SerializeField] private string number;

    private void OnMouseDown()
    {
        ReactionManager.Call("OpenDoorButton", number);
    }
}
