using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Assets.Scripts.ActionReactionSystem;


public class clickSymbolScreen : MonoBehaviour
{
    public string textureName;

    public bool charIsClose;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            charIsClose = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
        {
            charIsClose = false;
        }
    }

    private void Update()
    {
        if (charIsClose == true && Input.GetKeyDown(KeyCode.E))
        {
            ReactionManager.Call("ReceiveSymbol", textureName);
        }
    }
}
