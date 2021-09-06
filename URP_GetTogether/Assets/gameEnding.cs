using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Assets.Scripts.ActionReactionSystem;

public class gameEnding : MonoBehaviour
{
    public GameObject Core;

    public GameObject colorChanger;

    public bool gameFinished;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        gameFinished = false;

        ReactionManager.Add("ChangeColors", ChangeColors);
        ReactionManager.Add("GameFinished", GameFinished);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P)) { 
            ReactionManager.Call("GameFinished");
            ReactionManager.Call("StartCutScene");
        }*/

        if (gameFinished == true)
        {
            ReactionManager.Call("ChangeColors");
        }

    }

    private void ChangeColors(string[] textureName)
    {
        colorChanger.SetActive(true);
    }

    private void GameFinished(string[] textureName)
    {
        gameFinished = true;
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("ChangeColors", ChangeColors);
        ReactionManager.Remove("GameFinished", GameFinished);
    }
}
