using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera3D))]

public class switchPerspective : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        var camera = GetComponent<Camera3D>();

        var player = PlayerId.LocalPlayer;
        if (player == null)
        {
            Debug.Log("No player found!");
            return;
        }

        if (Input.GetKeyDown("v"))
        {
            if (camera.cameraMode == Camera3D.CameraMode.ThirdPerson)
            {
                camera.cameraMode = Camera3D.CameraMode.FirstPerson;
            }
            else
                camera.cameraMode = Camera3D.CameraMode.ThirdPerson;
        }

        /*if(camera.distanceToTarget == 0)
        {
            camera.cameraMode = Camera3D.CameraMode.FirstPerson;
        }
        else if (camera.distanceToTarget != 0)
        {
            camera.cameraMode = Camera3D.CameraMode.ThirdPerson;
        }*/
    }
}
