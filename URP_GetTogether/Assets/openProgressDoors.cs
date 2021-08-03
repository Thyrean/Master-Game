using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ActionReactionSystem;

public class openProgressDoors : MonoBehaviour
{
    public bool charIsClose;

    public GameObject[] images;
    public GameObject[] doors;
    public GameObject[] batteryPads;
    public GameObject[] riddles;

    public Material dissolve;

    public bool firstRiddleComplete, secondRiddleComplete, thirdRiddleComplete, forthRiddleComplete;


    // Update is called once per frame
    private void Start()
    {
        firstRiddleComplete = false;
        secondRiddleComplete = false;
        thirdRiddleComplete = false;
        forthRiddleComplete = false;

        charIsClose = false;

        ReactionManager.Add("OpenDoor", OpenDoor);

        ReactionManager.Add("FirstBatteryPlaced", FirstBatteryPlaced);
        ReactionManager.Add("SecondBatteryPlaced", SecondBatteryPlaced);
        ReactionManager.Add("ThirdBatteryPlaced", ThirdBatteryPlaced);
        //ReactionManager.Add("FourthBatteryPlaced", FourthBatteryPlaced);
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
        }
    }

    private void SecondBatteryPlaced(string[] empty)
    {
        if (secondRiddleComplete == false)
        {
            images[4].SetActive(false);
            images[2].SetActive(true);

            secondRiddleComplete = true;
        }
    }

    private void ThirdBatteryPlaced(string[] empty)
    {
        if (thirdRiddleComplete == false)
        {
            images[4].SetActive(false);
            images[3].SetActive(true);

            thirdRiddleComplete = true;
        }
    }

    private void OpenDoor(string[] empty)
    {
        if (firstRiddleComplete == true && doors[0].activeSelf == true)
        {
            Debug.Log("Opening First Door");

            doors[0].GetComponent<Animation>().Play("DoorDissolve");

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

            batteryPads[1].tag = "Untagged";
            batteryPads[2].tag = "batteryPad2";

            images[2].SetActive(false);
            images[4].SetActive(true);

            riddles[0].SetActive(true);

            StartCoroutine(DestroyDoor(doors[1]));
        }

        if (thirdRiddleComplete == true && doors[2].activeSelf == true)
        {
            doors[2].GetComponent<Animation>().Play("DoorDissolve");

            batteryPads[2].tag = "Untagged";
            batteryPads[3].tag = "batteryPad3";

            images[3].SetActive(false);
            images[4].SetActive(true);
            
            StartCoroutine(DestroyDoor(doors[2]));
        }
    }

    IEnumerator DestroyDoor(GameObject go)
    {
        yield return new WaitForSeconds(2);

        go.SetActive(false);
    }
}
