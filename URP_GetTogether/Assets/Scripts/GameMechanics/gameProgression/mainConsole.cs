using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ActionReactionSystem;

public class mainConsole : MonoBehaviour
{
    public bool charIsClose;

    public GameObject[] images;
    public GameObject[] doors;

    public GameObject[] upperDoors;

    public GameObject[] batteryPads;
    public GameObject[] riddles;

    public GameObject[] missingBatteryUI;
    public GameObject[] doorLockedUI;

    public Animator groundAnimator;
    
    //public Texture[] groundEmissionMaps;

    public Material dissolve;

    public bool firstRiddleComplete, secondRiddleComplete, thirdRiddleComplete, fourthRiddleComplete;

    public GameObject[] players;

    // Update is called once per frame
    private void Start()
    {
        firstRiddleComplete = false;
        secondRiddleComplete = false;
        thirdRiddleComplete = false;
        fourthRiddleComplete = false;

        players = GameObject.FindGameObjectsWithTag("Character");
        for(var i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PickUpObject>().batteryPads[0] = batteryPads[0];
            players[i].GetComponent<PickUpObject>().batteryPads[1] = batteryPads[1];
            players[i].GetComponent<PickUpObject>().batteryPads[2] = batteryPads[2];
            players[i].GetComponent<PickUpObject>().batteryPads[3] = batteryPads[3];
        }

        batteryPads[1].tag = "Untagged";
        batteryPads[2].tag = "Untagged";
        batteryPads[3].tag = "Untagged";

        charIsClose = false;

        ReactionManager.Add("OpenDoor", OpenDoor);

        ReactionManager.Add("FirstBatteryPlaced", FirstBatteryPlaced);
        ReactionManager.Add("SecondBatteryPlaced", SecondBatteryPlaced);
        ReactionManager.Add("ThirdBatteryPlaced", ThirdBatteryPlaced);
        ReactionManager.Add("FourthBatteryPlaced", FourthBatteryPlaced);
    }
    void Update()
    {
        if (charIsClose && Input.GetKeyDown(KeyCode.E) && firstRiddleComplete == true)
        {
            ReactionManager.Call("OpenDoor");
        }

        if (charIsClose && Input.GetKeyDown(KeyCode.E) && secondRiddleComplete == true)
        {
            ReactionManager.Call("OpenDoor");
        }

        if (charIsClose && Input.GetKeyDown(KeyCode.E) && thirdRiddleComplete == true)
        {
            ReactionManager.Call("OpenDoor");
        }

        if (charIsClose && Input.GetKeyDown(KeyCode.E) && fourthRiddleComplete == true)
        {
            ReactionManager.Call("OpenDoor");
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

    private void FirstBatteryPlaced(string[] empty)
    {
        if (firstRiddleComplete == false)
        {
            images[0].SetActive(false);
            images[1].SetActive(true);

            firstRiddleComplete = true;

            groundAnimator.Play("FirstPattern");

            missingBatteryUI[0].SetActive(false);
            missingBatteryUI[1].SetActive(false);
            missingBatteryUI[2].SetActive(false);

            doorLockedUI[0].SetActive(true);
            doorLockedUI[1].SetActive(true);
            doorLockedUI[2].SetActive(true);
        }
    }

    private void SecondBatteryPlaced(string[] empty)
    {
        if (secondRiddleComplete == false)
        {
            images[4].SetActive(false);
            images[2].SetActive(true);

            secondRiddleComplete = true;

            groundAnimator.Play("SecondPattern");

            missingBatteryUI[3].SetActive(false);
            missingBatteryUI[4].SetActive(false);
            missingBatteryUI[5].SetActive(false);
        }
    }

    private void ThirdBatteryPlaced(string[] empty)
    {
        if (thirdRiddleComplete == false)
        {
            images[4].SetActive(false);
            images[3].SetActive(true);

            thirdRiddleComplete = true;

            groundAnimator.Play("ThirdPattern");
        }
    }

    private void FourthBatteryPlaced(string[] empty)
    {
        if (fourthRiddleComplete == false)
        {
            images[4].SetActive(false);
            images[5].SetActive(true);

            fourthRiddleComplete = true;

            groundAnimator.Play("FourthPattern");
        }
    }

    private void OpenDoor(string[] empty)
    {
        if (firstRiddleComplete == true && doors[0].activeSelf == true)
        {
            Debug.Log("Opening First Door");

            doors[0].GetComponent<Animation>().Play("DoorDissolve");
            doors[0].GetComponent<AudioSource>().PlayDelayed(.6f);

            batteryPads[0].tag = "Untagged";
            batteryPads[1].tag = "batteryPad1";

            images[1].SetActive(false);
            images[4].SetActive(true);

            StartCoroutine(DestroyDoor(doors[0]));
        }

        if(secondRiddleComplete == true && doors[1].activeSelf == true)
        {
            Debug.Log("Opening Second Door");

            doors[1].GetComponent<Animation>().Play("DoorDissolve");
            doors[1].GetComponent<AudioSource>().PlayDelayed(.6f);

            batteryPads[1].tag = "Untagged";
            batteryPads[2].tag = "batteryPad2";

            images[2].SetActive(false);
            images[4].SetActive(true);

            riddles[0].GetComponent<symbolRiddle>().enabled = true;

            StartCoroutine(DestroyDoor(doors[1]));
        }

        if (thirdRiddleComplete == true && doors[2].activeSelf == true)
        {
            doors[2].GetComponent<Animation>().Play("DoorDissolve");
            doors[2].GetComponent<AudioSource>().PlayDelayed(.6f);

            batteryPads[2].tag = "Untagged";
            batteryPads[3].tag = "batteryPad3";

            images[3].SetActive(false);
            images[4].SetActive(true);
            
            StartCoroutine(DestroyDoor(doors[2]));

            upperDoors[0].GetComponent<Animation>().Play("DoorDissolve");
            doors[2].GetComponent<AudioSource>().PlayDelayed(.6f);
            StartCoroutine(DestroyDoor(upperDoors[0]));
        }

        if (fourthRiddleComplete == true && doors[3].activeSelf == true)
        {
            doors[3].GetComponent<Animation>().Play("DoorDissolve");
            doors[3].GetComponent<AudioSource>().PlayDelayed(.6f);

            batteryPads[3].tag = "Untagged";

            images[3].SetActive(false);
            images[4].SetActive(true);

            StartCoroutine(DestroyDoor(doors[3]));


            upperDoors[1].GetComponent<Animation>().Play("DoorDissolve");
            upperDoors[1].GetComponent<AudioSource>().PlayDelayed(.6f);
            StartCoroutine(DestroyDoor(upperDoors[1]));
        }
    }

    IEnumerator DestroyDoor(GameObject go)
    {
        yield return new WaitForSeconds(6);

        go.SetActive(false);
    }
}
