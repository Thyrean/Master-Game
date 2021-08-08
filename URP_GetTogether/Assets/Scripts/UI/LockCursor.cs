using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LockCursor : MonoBehaviour
{
    // Start is called before the first frame update

    /*private void Update()
    {
        Cursor.visible = true;

    }*/

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;

        //SceneManager.sceneLoaded += updateCursor;
    }

    /*private void Start()
    {
        Cursor.visible = true;

        SceneManager.sceneLoaded += updateCursor;
    }*/

    private void updateCursor(Scene S, LoadSceneMode E)
    {
        Cursor.visible = true;

        Cursor.lockState = CursorLockMode.Locked;
    }
}

