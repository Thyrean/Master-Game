 using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;
using Lightbug.CharacterControllerPro.Core;

using Lightbug.CharacterControllerPro.Demo;
using UnityEngine;
using Mirror;
using System;

public class PickUpObject : NetworkBehaviour
{
    public Animator anim;
    public GameObject myHands;

    public GameObject orbConsole;

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

    [SyncVar]
    public bool atOrbConsole;


    [SyncVar]
    public bool firstOrbPlaced;


    public GameObject[] batteryPads;

    public bool canpickup;
    GameObject ObjectIwantToPickUp;
    public bool hasItem;

    /*
    public GameObject batteryUI;

    public Material fullMaterial;
    public Material halfMaterial;
    public Material emptyMaterial;

    [SyncVar(hook = "UpdateBattery")]
    public int batteryCharge;
    */

    void Start()
    {
        atPad = 0;
        canpickup = false;
        hasItem = false;
        // batteryCharge = 100;

        //      batteryUI.GetComponent<Renderer>().material = fullMaterial;

        firstOrbPlaced = false;
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

        if (Input.GetKeyDown(KeyCode.O) && hasItem == true)
        {
            if (isClient)
                ClientDropItem(ObjectIwantToPickUp.GetComponent<NetworkIdentity>().netId);
        }

        if (Input.GetKeyDown(KeyCode.E) && hasItem == true && (atBatteryPad0 == true || atBatteryPad1 == true || atBatteryPad2 == true || atBatteryPad3 == true))
        {
            if (isClient)
                ClientPlaceBattery(ObjectIwantToPickUp.GetComponent<NetworkIdentity>().netId);
        }

        if(Input.GetKeyDown(KeyCode.E) && hasItem == true && ObjectIwantToPickUp.tag == "CarriedOrb" && atOrbConsole == true)
        {
            if (isClient)
                ClientPlaceOrb(ObjectIwantToPickUp.GetComponent<NetworkIdentity>().netId);
        }

        /*if(hasItem == true)
        {
            actor.CurrentState;
        }*/

            /*if(isServer && hasItem == true)
            {
                UpdateItemPosition(ObjectIwantToPickUp, myHands);
            }*/

    }

    /*private void UpdateBattery(int oldValue, int newValue)
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
    }*/

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

        if(other.gameObject.tag == "InsertOrb" || other.gameObject.tag == "orbConsole")
        {
            atOrbConsole = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Battery")
        {
            canpickup = false;
            //ObjectIwantToPickUp = null;
        }

        if (other.gameObject.tag == "Orb")
        {
            canpickup = false;
            //ObjectIwantToPickUp = null;
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

        if (other.gameObject.tag == "InsertOrb" || other.gameObject.tag == "orbConsole")
        {
            atOrbConsole = false;
        }
    }

    [Command]
    public void ClientPickUpItem(uint itemID)
    {
        atBatteryPad0 = false;
        atBatteryPad1 = false;
        atBatteryPad2 = false;
        atBatteryPad3 = false;

        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        Collider cc = pickUpObj.GetComponent<Collider>();
        cc.enabled = false;

        if(pickUpObj.GetComponent<Rigidbody>() != null)
            pickUpObj.GetComponent<Rigidbody>().isKinematic = true;

        //pickUpObj.tag = "disabled";
        pickUpObj.transform.position = myHands.transform.position;
        pickUpObj.transform.parent = myHands.transform;
        if (pickUpObj.CompareTag("Battery"))
        {
            pickUpObj.transform.localPosition = new Vector3(0.00590000022f, 0.0046000001f, 0.0119000003f);
            pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            pickUpObj.transform.localRotation = Quaternion.Euler(31.3597622f, 46.4170151f, 218.755554f);
        }
        if (pickUpObj.CompareTag("Orb"))
        {
            pickUpObj.transform.localPosition = new Vector3(0.0060200002f, -0.00164000003f, 0.00731999986f);
            pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            pickUpObj.transform.localRotation = Quaternion.Euler(333.75827f, 233.267761f, 137.937958f);

            pickUpObj.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);
        }

        //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = false;
        //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = true; 
        //

        //UpdateBattery(batteryCharge, batteryCharge - 50);

        anim.Play("CarryIdle");
        hasItem = true;

        ServerPickUpItem(itemID);
        
    }

    [ClientRpc]
    public void ServerPickUpItem(uint itemID)
    {
        atBatteryPad0 = false;
        atBatteryPad1 = false;
        atBatteryPad2 = false;
        atBatteryPad3 = false;

        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        Collider cc = pickUpObj.GetComponent<Collider>();
        cc.enabled = false;

        //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = false;
        //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = true;

        if (pickUpObj.GetComponent<Rigidbody>() != null)
            pickUpObj.GetComponent<Rigidbody>().isKinematic = true;

        //pickUpObj.tag = "disabled";
        pickUpObj.transform.position = myHands.transform.position;
        pickUpObj.transform.parent = myHands.transform;
        if (pickUpObj.CompareTag("Battery"))
        {
            pickUpObj.transform.localPosition = new Vector3(0.00590000022f, 0.0046000001f, 0.0119000003f);
            pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            pickUpObj.transform.localRotation = Quaternion.Euler(31.3597622f, 46.4170151f, 218.755554f);
        }
        if (pickUpObj.CompareTag("Orb"))
        {
            pickUpObj.transform.localPosition = new Vector3(0.0060200002f, -0.00164000003f, 0.00731999986f);
            pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);
            pickUpObj.transform.localRotation = Quaternion.Euler(333.75827f, 233.267761f, 137.937958f);

            pickUpObj.GetComponent<BoxCollider>().size = new Vector3(0.1f, 0.1f, 0.1f);

            pickUpObj.tag = "CarriedOrb";
        }

        //UpdateBattery(batteryCharge, batteryCharge - 50);


        anim.Play("CarryIdle");
        hasItem = true;
        
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

        Collider cc = pickUpObj.GetComponent<Collider>();
        cc.enabled = true;

        //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = true;
        //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = false;

        if (pickUpObj.GetComponent<Rigidbody>() != null)
            pickUpObj.GetComponent<Rigidbody>().isKinematic = false;

        //pickUpObj.tag = "Battery";
        pickUpObj.transform.parent = null;

        if (pickUpObj.CompareTag("CarriedOrb")) { 
            pickUpObj.tag = "Orb";


            pickUpObj.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
        }

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

        Collider cc = pickUpObj.GetComponent<Collider>();
        cc.enabled = true;

        //ObjectIwantToPickUp.GetComponent<NetworkTransform>().enabled = true;
        //ObjectIwantToPickUp.GetComponent<NetworkTransformChild>().enabled = false;

        if (pickUpObj.GetComponent<Rigidbody>() != null)
            pickUpObj.GetComponent<Rigidbody>().isKinematic = false;

        //pickUpObj.tag = "Battery";
        pickUpObj.transform.parent = null;

        if (pickUpObj.CompareTag("CarriedOrb"))
        {

            pickUpObj.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
            pickUpObj.tag = "Orb";
        }

        anim.Play("StableGrounded");
        hasItem = false;
        canpickup = false;

    }

    [Command]
    public void ClientPlaceBattery(uint itemID)
    {
        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        Collider cc = pickUpObj.GetComponent<Collider>();
        cc.enabled = true;

        if (pickUpObj.GetComponent<Rigidbody>() != null)
            pickUpObj.GetComponent<Rigidbody>().isKinematic = true;

        pickUpObj.transform.position = batteryPads[atPad].transform.position + new Vector3(0, 0f, 0);
        pickUpObj.transform.rotation = Quaternion.Euler(0, -90f, 0);
        pickUpObj.transform.parent = null;
        //pickUpObj.tag = "disabled";

        anim.Play("StableGrounded");
        hasItem = false;
        canpickup = false;

        batteryPads[atPad].GetComponent<EnergyPad>().batteryPlaced = true;
        //batteryPads[atPad].GetComponent<EnergyPad>().ObjectUI.SetActive(false);
        //batteryPads[atPad].tag = "disabled";

        atBatteryPad0 = false;
        atBatteryPad1 = false;
        atBatteryPad2 = false;
        atBatteryPad3 = false;

        ServerPlaceBattery(itemID);
    }

    [ClientRpc]
    private void ServerPlaceBattery(uint itemID)
    {
        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        Collider cc = pickUpObj.GetComponent<Collider>();
        cc.enabled = true;

        if (pickUpObj.GetComponent<Rigidbody>() != null)
            pickUpObj.GetComponent<Rigidbody>().isKinematic = true;

        pickUpObj.transform.position = batteryPads[atPad].transform.position + new Vector3(0, 0f, 0);
        pickUpObj.transform.rotation = Quaternion.Euler(0, -90f, 0);
        pickUpObj.transform.parent = null;
        //pickUpObj.tag = "disabled";

        //batteryPad.GetComponent<EnergyPad>().batteryPlaced = true;
        //batteryPad.GetComponent<EnergyPad>().ObjectUI.SetActive(false);

        anim.Play("StableGrounded");
        hasItem = false;
        canpickup = false;

        batteryPads[atPad].GetComponent<EnergyPad>().batteryPlaced = true;

        atBatteryPad0 = false;
        atBatteryPad1 = false;
        atBatteryPad2 = false;
        atBatteryPad3 = false;

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

    
    [Command]
    public void ClientPlaceOrb(uint itemID)
    {
        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        Collider cc = pickUpObj.GetComponent<Collider>();
        cc.enabled = true;

        if (firstOrbPlaced == true)
        {
            pickUpObj.transform.parent = orbConsole.transform;
            pickUpObj.transform.position = new Vector3(0, 0, 0);
            pickUpObj.transform.localPosition = new Vector3(-12.8299999f, 17.0799999f, -5.46999979f);

            pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (firstOrbPlaced == false)
        {
            pickUpObj.transform.parent = orbConsole.transform;
            pickUpObj.transform.position = new Vector3(0, 0, 0);
            pickUpObj.transform.localPosition = new Vector3(-12.8299999f, 17.0799999f, 5.51999998f);

            pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        anim.Play("StableGrounded");
        hasItem = false;
        canpickup = false;

        ServerPlaceOrb(itemID);
    }

    [ClientRpc]
    public void ServerPlaceOrb(uint itemID)
    {
        GameObject pickUpObj = NetworkIdentity.spawned[itemID].gameObject;

        Collider cc = pickUpObj.GetComponent<Collider>();
        cc.enabled = true;

        if (firstOrbPlaced == true)
        {
            pickUpObj.transform.parent = orbConsole.transform;
            pickUpObj.transform.position = new Vector3(0, 0, 0);
            pickUpObj.transform.localPosition = new Vector3(-12.8299999f, 17.0799999f, -5.46999979f);

            pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        if (firstOrbPlaced == false)
        {
            pickUpObj.transform.parent = orbConsole.transform;
            pickUpObj.transform.position = new Vector3(0, 0, 0);
            pickUpObj.transform.localPosition = new Vector3(-12.8299999f, 17.0799999f, 5.51999998f);

            pickUpObj.transform.rotation = Quaternion.Euler(0, 0, 0);

            //firstOrbPlaced = true;

            ReactionManager.Call("FirstOrb");
        }


        anim.Play("StableGrounded");
        hasItem = false;
        canpickup = false;

        pickUpObj.tag = "Untagged";
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