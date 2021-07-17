using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PickUpObject : NetworkBehaviour
{
    public Animator anim;
    public GameObject myHands;
    bool canpickup;
    GameObject ObjectIwantToPickUp;
    bool hasItem;

    public GameObject batteryUI;

    [SyncVar(hook = "UpdateBattery")]
    public int batteryCharge;

    void Start()
    {
        canpickup = false;
        hasItem = false;
        batteryCharge = 100;
    }

    void LateUpdate()
    {
        if (canpickup == true) 
        { 
            if (Input.GetKeyDown(KeyCode.E) && hasItem == false)  
            {
                
                if (isClient)
                    ClientPickUpItem(ObjectIwantToPickUp.GetComponent<NetworkIdentity>().netId);
                //if (isServer)
                    //ServerPickUpItem(ObjectIwantToPickUp.GetComponent<NetworkIdentity>().netId);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && hasItem == true) 
        {
            if (isClient)
                ClientDropItem(ObjectIwantToPickUp.GetComponent<NetworkIdentity>().netId);
        }

        
        /*if(isServer && hasItem == true)
        {
            UpdateItemPosition(ObjectIwantToPickUp, myHands);
        }*/

    }
    private void UpdateBattery(int oldValue, int newValue)
    {
        if (oldValue == newValue)
            return;

        batteryCharge = newValue;

        if(newValue == 50)
        {
            batteryUI.transform.localScale = new Vector3(1f, 0.5f, 1f);

            Debug.Log("Battery is 50% charged");
        }
        else if (newValue == 0)
        {
            batteryUI.transform.localScale = new Vector3(1f, 0f, 1f);

            Debug.Log("Battery is 0% charged");
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Battery") 
        {
            canpickup = true;  
            ObjectIwantToPickUp = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        canpickup = false; 

    }

    /*private void UpdateItemPosition(GameObject _Object, GameObject _copyObject)
    {
        _Object.transform.position = _copyObject.transform.position;
    }*/

    [Command]
    public void ClientPickUpItem(uint itemID)
    {
        if (batteryCharge >= 50)
        {
            GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

            CapsuleCollider cc = pickUpObj.GetComponent<CapsuleCollider>();
            cc.enabled = false;

            pickUpObj.GetComponent<Rigidbody>().isKinematic = true;
            pickUpObj.transform.position = myHands.transform.position - new Vector3(.5f, .15f, .5f);
            pickUpObj.transform.parent = myHands.transform;

            //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = false;
            //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = true; 
            //

            UpdateBattery(batteryCharge, batteryCharge - 50);

            //anim.Play("CarryIdle");
            hasItem = true;

            ServerPickUpItem(itemID);
        }
        else return;
    }

    [ClientRpc]
    public void ServerPickUpItem(uint itemID)
    {
        if (batteryCharge >= 50)
        {
            GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

            CapsuleCollider cc = pickUpObj.GetComponent<CapsuleCollider>();
            cc.enabled = false;

            //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = false;
            //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = true;

            pickUpObj.GetComponent<Rigidbody>().isKinematic = true;
            pickUpObj.transform.position = myHands.transform.position - new Vector3(.5f,.15f,.5f);
            pickUpObj.transform.parent = myHands.transform;

            //anim.Play("CarryIdle");
            hasItem = true;
        }
        else return;
    }

    [Command]
    private void ClientDropItem(uint itemID)
    {
        /*CapsuleCollider[] cc = ObjectIwantToPickUp.GetComponents<CapsuleCollider>();
            foreach (CapsuleCollider c in cc)
            {
                c.enabled = true;
            }*/
        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        CapsuleCollider cc = pickUpObj.GetComponent<CapsuleCollider>();
        cc.enabled = true;

        //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = true;
        //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = false;

        pickUpObj.GetComponent<Rigidbody>().isKinematic = false;
        pickUpObj.transform.parent = null;

        //anim.Play("StableGrounded");
        hasItem = false; 

        ServerDropItem(itemID);
    }

    [ClientRpc]
    private void ServerDropItem(uint itemID)
    {
        /*CapsuleCollider[] cc = ObjectIwantToPickUp.GetComponents<CapsuleCollider>();
            foreach (CapsuleCollider c in cc)
            {
                c.enabled = true;
            }*/
        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        CapsuleCollider cc = pickUpObj.GetComponent<CapsuleCollider>();
        cc.enabled = true;

        //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = true;
        //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = false;

        pickUpObj.GetComponent<Rigidbody>().isKinematic = false;
        pickUpObj.transform.parent = null;

        // anim.Play("StableGrounded");
        hasItem = false;
    }
}