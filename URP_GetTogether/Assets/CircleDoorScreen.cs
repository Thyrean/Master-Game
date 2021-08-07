using Assets.Scripts.ActionReactionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class CircleDoorScreen : MonoBehaviour
{
    public GameObject connectedDoor;
    public GameObject nextDoor;

    //public bool nextDoorOpened; 

    public bool charIsClose;


    // Start is called before the first frame update
    void Start()
    {
        //nextDoorOpened = false;

        ReactionManager.Add("openCircleDoor", OpenCircleDoor);
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

    private void Update()
    {
        if (charIsClose == true && Input.GetKeyDown(KeyCode.E)) { 
            ReactionManager.Call("openCircleDoor");
        }
    }

    private void OpenCircleDoor(string[] empty)
    {
        //if(connectedDoor.GetComponent<rotateDoor>().riddleSolved == true)
        //{
        //connectedDoor.GetComponent<rotateDoor>().enabled = false;

        //if (nextDoor != null && nextDoorOpened == false){

        //nextDoorOpened = true;

        connectedDoor.GetComponent<Animator>().Play("openCircleDoor");
        
        if (nextDoor != null)
            nextDoor.SetActive(true);

        if (gameObject.activeSelf == true)
        {
            //StartCoroutine(DisableGO(connectedDoor));
            //StartCoroutine(DisableGO(gameObject));

            Destroy(connectedDoor, 2f);
            Destroy(gameObject, 2.5f);
        }
        //}
    }

    IEnumerator DisableGO(GameObject disableGo)
    {
        Debug.Log("HEY COROUTINE WORKING!!!!");
        yield return new WaitForSeconds(2);

        disableGo.SetActive(false);
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("openCircleDoor", OpenCircleDoor);
    }
}
