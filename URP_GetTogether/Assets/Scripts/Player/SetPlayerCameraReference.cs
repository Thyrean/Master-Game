using Lightbug.CharacterControllerPro.Demo;
using Lightbug.CharacterControllerPro.Core;
using Lightbug.CharacterControllerPro.Implementation;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera3D))]

public class SetPlayerCameraReference : MonoBehaviour
{
    private void Awake()
    {
        var camera = GetComponent<Camera3D>();

        var player = PlayerId.LocalPlayer;
        if (player == null)
        {
            Debug.Log("No player found!");
            return;
        }

        camera.targetTransform = player.transform.Find("Graphics").transform;

        //camera.targetTransform = player.transform;
        camera.bodyObject = player.gameObject;
    }

}
