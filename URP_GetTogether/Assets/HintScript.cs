using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ActionReactionSystem;
public class HintScript : MonoBehaviour
{
    public bool charIsClose;

    public mainConsole console;

    public GameObject firstHint;
    public GameObject secondHint;
    public GameObject thirdHint;
//    public GameObject fourthHint;

    // Start is called before the first frame update
    void Start()
    {
        ReactionManager.Add("ShowHint", ShowHint);
    }

    // Update is called once per frame
    void Update()
    {
        if (firstHint.activeSelf && console.secondRiddleComplete == true)
            firstHint.SetActive(false);

        if (secondHint.activeSelf && console.thirdRiddleComplete == true)
            secondHint.SetActive(false);

        if (thirdHint.activeSelf && console.fourthRiddleComplete == true)
            thirdHint.SetActive(false);

        if (charIsClose == true && Input.GetKeyDown(KeyCode.E))
        {
            ReactionManager.Call("ShowHint");
        }
    }

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

    private void ShowHint(string[] empty)
    {
        if(console.firstRiddleComplete == true && console.secondRiddleComplete == false)
        {
            firstHint.SetActive(true);
        }

        if (console.firstRiddleComplete == true && console.secondRiddleComplete == true &&
            console.thirdRiddleComplete == false && console.fourthRiddleComplete == false)
        {
            secondHint.SetActive(true);
        }

        if (console.firstRiddleComplete == true && console.secondRiddleComplete == true &&
            console.thirdRiddleComplete == true && console.fourthRiddleComplete == false)
        {
            thirdHint.SetActive(true);
        }

        /*if (console.firstRiddleComplete == true && console.secondRiddleComplete == true &&
            console.thirdRiddleComplete == true && console.fourthRiddleComplete == true)
        {
            fourthHint.SetActive(true);
        }*/
    }
}
