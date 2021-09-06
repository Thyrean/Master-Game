using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugTeleporter : MonoBehaviour
{
    public GameObject Teleporter1;

    public GameObject Teleporter2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if(Teleporter1.activeSelf == false)
                Teleporter1.SetActive(true);

            if (Teleporter2.activeSelf == false)
                Teleporter2.SetActive(true);
        }
    }
}
