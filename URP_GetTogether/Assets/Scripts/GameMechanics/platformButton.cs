using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnMouseDown()
    {
        if(gameObject.tag == "redButton")
            ReactionManager.Call("moveRed");
        else if (gameObject.tag == "blueButton")
            ReactionManager.Call("moveBlue");
        else if (gameObject.tag == "greenButton")
            ReactionManager.Call("moveGreen");
    }
}
