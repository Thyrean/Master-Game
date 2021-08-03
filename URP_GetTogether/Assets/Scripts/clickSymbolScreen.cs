using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Assets.Scripts.ActionReactionSystem;


public class clickSymbolScreen : MonoBehaviour
{
    public string textureName;

    // Update is called once per frame
    private void OnMouseDown()
    {
        ReactionManager.Call("ReceiveSymbol", textureName);
    }
}
