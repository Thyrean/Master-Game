using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.ActionReactionSystem;
using Lightbug.CharacterControllerPro.Core;

using Lightbug.CharacterControllerPro.Demo;
using Mirror;

public class laserMovement : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;

    public GameObject resetPosGO;
    public Vector3 resetPosition;

    public float speed = 1.0F;
    public float waitTime = 1.0F;

    void Start()
    {
        resetPosition = resetPosGO.transform.position;
    }
    
    void Update()
    {
        if(startMarker != null && endMarker != null)
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, Mathf.PingPong((Time.time * speed), waitTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Character"))
        {
            other.gameObject.GetComponent<CharacterActor>().Teleport(resetPosition);
            //other.gameObject.GetComponent<CharacterActor>().Rotation = Quaternion.Euler(new Vector3(0, 90f, 0));

            //charCam.GetComponent<Camera3D>().viewReference = Quaternion.Euler(new Vector3(0, 90f, 0));

            //charCam.GetComponent<Camera3D>().transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
            //charCam.transform.rotation = Quaternion.Euler(new Vector3(0, 90f, 0));
        }
    }
}
