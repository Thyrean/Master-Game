using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UI;
    public GameObject text;

    public void Start()
    {
        UI.SetActive(true);
        text.GetComponent<TextMeshProUGUI>().text = "Move = W/A/S/D\n Interact = E\n Jump = Spacebar\n Crouch = CTRL\n Sprint = SHIFT\n 1st/3rd Person = V";
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "firstDoor")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Use the keyboard buttons 1, 2 and 3 to rotate and align the star constellation correctly";
        }

        if (other.tag == "distanceCollider")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Maneuver through the maze together. Do not move further than 1 square from each other.";
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (UI.activeSelf == true)
        {
            UI.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            UI.SetActive(false);
        }
    }
}
