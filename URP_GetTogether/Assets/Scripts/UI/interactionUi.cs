using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactionUi : MonoBehaviour
{
    public GameObject UI;
    public GameObject text;

    public Component pickUpLogic;

    public void Start()
    {
        pickUpLogic = gameObject.GetComponent<PickUpObject>();

        UI.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Battery") || other.CompareTag("Orb")) && pickUpLogic.GetComponent<PickUpObject>().hasItem == false)
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Pick Up";
        }

        if(other.tag == "batteryPad0" || other.tag == "batteryPad1" || other.tag ==  "batteryPad2" || other.tag == "batteryPad3")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Insert Battery";
        }

        if (other.tag == "TouchConsole")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Use Console";
        }

        if (other.tag == "Interact")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Interact";
        }

        if (other.tag == "Touch")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Touch";
        }

        if (other.tag == "orbConsole")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Insert Orb";
        }

        if(other.tag == "Hint")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Request Hint";
        }

        if(other.tag == "Select")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Select Symbol";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (UI.activeSelf == true && other.tag != "CarriedOrb" && other.tag != "Untagged")
        {
            UI.SetActive(false);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            UI.SetActive(false);
        }
    }
}
