using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPad : MonoBehaviour
{
    public bool charIsClose = false;
    public GameObject HoverUI;

    private void Start()
    {
        HoverUI.SetActive(false);
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
        if (charIsClose == true)
        {
            HoverUI.SetActive(true);
        }
        else HoverUI.SetActive(false);

        if (charIsClose == true && Input.GetKeyDown(KeyCode.E))
        {
            HoverUI.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.E) || charIsClose == false)
        {

        }
    }
}
