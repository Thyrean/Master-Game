using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;

using UnityEngine;

public class EnergyPad : MonoBehaviour
{
    public bool charIsClose = false;
    //public GameObject HoverUI;
    //public GameObject ObjectUI;

    //public GameObject linkedDoor;

    public bool batteryPlaced;

    private void Start()
    {

        //ReactionManager.Add("ContinueProgress", ContinueProgress);
        //HoverUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        charIsClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
        charIsClose = false;
    }

    private void Update()
    {
        /*if (charIsClose == true)
        {
            HoverUI.SetActive(true);
        }
        else HoverUI.SetActive(false);*/

        if(batteryPlaced == true)
        {
            //linkedDoor.transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        if(gameObject.CompareTag("batteryPad0") && batteryPlaced == true)
        {
            ReactionManager.Call("FirstBatteryPlaced");
        }
        if (gameObject.CompareTag("batteryPad1") && batteryPlaced == true)
        {
            ReactionManager.Call("SecondBatteryPlaced");
        }
        if (gameObject.CompareTag("batteryPad2") && batteryPlaced == true)
        {
            ReactionManager.Call("ThirdBatteryPlaced");
        }
        if (gameObject.CompareTag("batteryPad3") && batteryPlaced == true)
        {
            ReactionManager.Call("FourthBatteryPlaced");
        }
    }

    private void ContinueProgress(string[] empty)
    {
    }

    /*
    private void AllowBatteryPlacement(string[] empty)
    {
        if (player != null)
        {
        battery.GetComponent<Rigidbody>().isKinematic = true;
        battery.transform.position = gameObject.transform.position + new Vector3(0f, 1f, 0f);
        battery.transform.rotation = Quaternion.Euler(0, 0, 0);
        battery.transform.parent = gameObject.transform;

        player.GetComponent<PickUpObject>().anim.Play("StableGrounded");

        batteryPlaced = true;
        ObjectUI.SetActive(false);
        }
    }*/
}
