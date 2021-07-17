using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showInteractivity : MonoBehaviour
{
    public bool charIsNear = false;

    public GameObject InteractUI;
    // Start is called before the first frame update
    void Start()
    {
        InteractUI.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        charIsNear = true;
    }

    private void OnTriggerExit(Collider other)
    {
        charIsNear = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (charIsNear == true)
        {
            InteractUI.SetActive(true);
        }
        else InteractUI.SetActive(false);
    }
}
