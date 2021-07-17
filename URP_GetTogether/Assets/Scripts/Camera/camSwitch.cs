using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.CharacterControllerPro.Demo;


public class camSwitch : MonoBehaviour
{
    public GameObject ConsoleCam;
    public GameObject CharCam;
    public GameObject Char;
    public GameObject TouchConsole;
    public Animator animator;

    public bool activeConsole;

    private void Awake()
    {
        SceneManager.sceneLoaded += FindObjects;
    }
    private void FindObjects(Scene S, LoadSceneMode E)
    {
        CharCam = GameObject.Find("CharCam");
        Char = gameObject;
        animator = Char.GetComponentInChildren<Animator>();
        TouchConsole = GameObject.Find("TouchConsole");

        /*for(int i = 0; i < ConsoleCam.Length; i++)
        {
            ConsoleCam[i].SetActive(false);
        }*/

        CharCam.SetActive(true);
    }

    void Update()
    {
        activeConsole = GameObject.Find("ConsoleCam");

        if (/*closeCheck == true && ConsoleCam.activeSelf == false&&*/Input.GetKeyDown(KeyCode.E))
        {
            ConsoleCam.SetActive(true);
            CharCam.SetActive(false);

            transform.Find("States").gameObject.SetActive(false);
            Char.GetComponent<Rigidbody>().isKinematic = true;
            animator.Play("Bored");

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else if (ConsoleCam.activeSelf == true && Input.GetKeyDown(KeyCode.E))
        {
            ConsoleCam.SetActive(false);
            CharCam.SetActive(true);

            transform.Find("States").gameObject.SetActive(true);
            Char.GetComponent<Rigidbody>().isKinematic = false;
            animator.Play("StableGrounded");
        }
    }
}

/*Transform GetChildDeep(Transform transform, string name)
{
    if (transform == null) return null;
    if (transform.name == name) return transform;
    var childCound = transform.ChildCount;
    for (var i = 0; i < childCount; i++)
    {
        var child = transform.GetChild(i);
        var childResult = GetChildDeep(child, name);
        if (childResult != null)
            return childResult;
    }
    return null;
}*/