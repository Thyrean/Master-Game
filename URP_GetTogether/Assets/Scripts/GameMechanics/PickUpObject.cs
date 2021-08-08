 using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;

using UnityEngine;
using Mirror;
using System;

public class PickUpObject : NetworkBehaviour
{
    public Animator anim;
    public GameObject myHands;

    [SyncVar]
    public bool atBatteryPad0;
    [SyncVar]
    public bool atBatteryPad1;
    [SyncVar]
    public bool atBatteryPad2;
    [SyncVar]
    public bool atBatteryPad3;

    [SyncVar]
    public int atPad;

    public GameObject[] batteryPads;

    public bool canpickup;
    GameObject ObjectIwantToPickUp;
    public bool hasItem;

    public GameObject batteryUI;

    public Material fullMaterial;
    public Material halfMaterial;
    public Material emptyMaterial;

    [SyncVar(hook = "UpdateBattery")]
    public int batteryCharge;

    void Start()
    {
        atPad = 0;
        canpickup = false;
        hasItem = false;
        batteryCharge = 100;

        batteryUI.GetComponent<Renderer>().material = fullMaterial;
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

        if (Input.GetKeyDown(KeyCode.E) && hasItem == true && (atBatteryPad0 == true || atBatteryPad1 == true || atBatteryPad2 == true || atBatteryPad3 == true))
        {
            if (isClient)
                ClientPlaceBattery(ObjectIwantToPickUp.GetComponent<NetworkIdentity>().netId);
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

        if (newValue == 50)
        {
            batteryUI.GetComponent<Renderer>().material = halfMaterial;

            Debug.Log("Battery is 50% charged");
        }
        else if (newValue == 0)
        {
            batteryUI.GetComponent<Renderer>().material = emptyMaterial;

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

        if (other.gameObject.tag == "Orb")
        {
            canpickup = true;
            ObjectIwantToPickUp = other.gameObject;
        }

        if (other.gameObject.tag == "batteryPad0")
        {
            atBatteryPad0 = true;
            ClientSendNumber(0);
        }
        if (other.gameObject.tag == "batteryPad1")
        {
            atBatteryPad1 = true;
            ClientSendNumber(1);
        }
        if (other.gameObject.tag == "batteryPad2")
        {
            atBatteryPad2 = true;
            ClientSendNumber(2);
        }
        if (other.gameObject.tag == "batteryPad3")
        {
            atBatteryPad3 = true;
            ClientSendNumber(3);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Battery")
        {
            canpickup = false;
        }

        if (other.gameObject.tag == "Orb")
        {
            canpickup = false;
        }

        if (other.gameObject.tag == "batteryPad0")
        {
            atBatteryPad0 = false;
            ClientSendNumber(0);
        }
        if (other.gameObject.tag == "batteryPad1")
        {
            atBatteryPad1 = false;
            ClientSendNumber(0);
        }
        if (other.gameObject.tag == "batteryPad2")
        {
            atBatteryPad2 = false;
            ClientSendNumber(0);
        }
        if (other.gameObject.tag == "batteryPad3")
        {
            atBatteryPad3 = false;
            ClientSendNumber(0);
        }
    }

    [Command]
    public void ClientPickUpItem(uint itemID)
    {
        if (batteryCharge >= 50)
        {
            GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

            CapsuleCollider cc = pickUpObj.GetComponent<CapsuleCollider>();
            cc.enabled = false;

            if(pickUpObj.GetComponent<Rigidbody>() != null)
                pickUpObj.GetComponent<Rigidbody>().isKinematic = true;

            //pickUpObj.tag = "disabled";
            pickUpObj.transform.position = myHands.transform.position - new Vector3(-0.2f, -0.2f, 0f);
            pickUpObj.transform.parent = myHands.transform;
            pickUpObj.transform.rotation = Quaternion.Euler(20, -20, 35);

            //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = false;
            //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = true; 
            //

            //UpdateBattery(batteryCharge, batteryCharge - 50);

            anim.Play("CarryIdle");
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

            if (pickUpObj.GetComponent<Rigidbody>() != null)
                pickUpObj.GetComponent<Rigidbody>().isKinematic = true;

            //pickUpObj.tag = "disabled";
            pickUpObj.transform.position = myHands.transform.position - new Vector3(-0.1f, -0.1f, 0f);
            pickUpObj.transform.parent = myHands.transform;
            pickUpObj.transform.rotation = Quaternion.Euler(20, -20, 35);

            //UpdateBattery(batteryCharge, batteryCharge - 50);


            anim.Play("CarryIdle");
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

        if (pickUpObj.GetComponent<Rigidbody>() != null)
            pickUpObj.GetComponent<Rigidbody>().isKinematic = false;

        //pickUpObj.tag = "Battery";
        pickUpObj.transform.parent = null;

        anim.Play("StableGrounded");
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

        if (pickUpObj.GetComponent<Rigidbody>() != null)
            pickUpObj.GetComponent<Rigidbody>().isKinematic = false;

        //pickUpObj.tag = "Battery";
        pickUpObj.transform.parent = null;

        anim.Play("StableGrounded");
        hasItem = false;
        canpickup = false;

    }

    [Command]
    public void ClientPlaceBattery(uint itemID)
    {
        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        CapsuleCollider cc = pickUpObj.GetComponent<CapsuleCollider>();
        cc.enabled = true;

        pickUpObj.GetComponent<Rigidbody>().isKinematic = true;
        pickUpObj.transform.position = batteryPads[atPad].transform.position + new Vector3(0, 1f, 0);
        pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        pickUpObj.transform.parent = null;
        //pickUpObj.tag = "disabled";

        anim.Play("StableGrounded");
        hasItem = false;
        canpickup = false;

        batteryPads[atPad].GetComponent<EnergyPad>().batteryPlaced = true;
        //batteryPads[atPad].GetComponent<EnergyPad>().ObjectUI.SetActive(false);
        //batteryPads[atPad].tag = "disabled";

        ServerPlaceBattery(itemID);
    }

    [ClientRpc]
    private void ServerPlaceBattery(uint itemID)
    {
        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        CapsuleCollider cc = pickUpObj.GetComponent<CapsuleCollider>();
        cc.enabled = true;

        pickUpObj.GetComponent<Rigidbody>().isKinematic = true;
        pickUpObj.transform.position = batteryPads[atPad].transform.position + new Vector3(0, 1f, 0);
        pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        pickUpObj.transform.parent = null;
        //pickUpObj.tag = "disabled";

        //batteryPad.GetComponent<EnergyPad>().batteryPlaced = true;
        //batteryPad.GetComponent<EnergyPad>().ObjectUI.SetActive(false);

        anim.Play("StableGrounded");
        hasItem = false;
        canpickup = false;

        batteryPads[atPad].GetComponent<EnergyPad>().batteryPlaced = true;

        pickUpObj.tag = "Untagged";
        //batteryPads[atPad].GetComponent<EnergyPad>().ObjectUI.SetActive(false);
        //batteryPads[atPad].tag = "disabled";
    }

    [Command]
    private void ClientSendNumber(int Number)
    {
        ServerShareNumber(Number);
    }
    [ClientRpc]
    private void ServerShareNumber(int shareNumber)
    {
        atPad = shareNumber;
    }

    /*[Command]
    private void ClientSendGO(GameObject syncGO)
    {
        if(syncGO == null)
        {
            Debug.Log("NO GO FOUND");
        }
        
        SendGOtoClients(syncGO);
    }

    [ClientRpc]
    private void SendGOtoClients(GameObject serverGO)
    {
        Debug.Log("Sending GO to all other Clients");

        batteryPad = serverGO;
    }*/

    /*private void ShareBatteryPad(string[] empty)
    {
        if(batteryPad != null)
        {
            batteryPad = batteryPad;
        }
    }*/
}