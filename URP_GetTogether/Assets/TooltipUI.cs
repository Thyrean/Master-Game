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
        UI.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CircleDoor")
        {
            UI.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "Use the buttons 1, 2 and 3 to rotate the door. If the correct pattern is met, ask your comrade for access authorization";
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UI.SetActive(false);
        }
    }
}
