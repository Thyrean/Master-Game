using UnityEngine.SceneManagement;
using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.CharacterControllerPro.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStateController))]
public class SetExternalReference : MonoBehaviour
{
    private void Awake()
    {
        SceneManager.sceneLoaded += SetReference;
    }
    private void SetReference(Scene S, LoadSceneMode E)
    {
        Debug.Log("Getting Reference");

        var m_stateController = GetComponent<CharacterStateController>(); 
        
        var camera = FindObjectOfType<Camera3D>();

        var player = PlayerId.LocalPlayer;
        if (player == null)
        {
            Debug.Log("No player found!");
            return;
        }

        if (camera != null)
        {
            m_stateController.MovementReferenceMode = MovementReferenceMode.External;
            m_stateController.ExternalReference = camera.transform;
        }
        else
        {
            Debug.Log("Camera is null!");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SetReference;
    }
}
