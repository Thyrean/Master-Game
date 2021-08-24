using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Assets.Scripts.ActionReactionSystem;
public class startCutScene : MonoBehaviour
{
    public GameObject charCamera;
    public GameObject player;

    public GameObject cutsceneCam;
    public bool cutSceneStarted;

    // Start is called before the first frame update
    void Start()
    {
        //cutsceneCam.SetActive(false);
        cutSceneStarted = false;

        ReactionManager.Add("StartCutScene", StartCutScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (cutsceneCam.GetComponent<Animation>().isPlaying == false && cutSceneStarted == true)
        {
            Debug.Log("Cutscene Finished");

            cutsceneCam.SetActive(false);
            charCamera.SetActive(true);

            Destroy(this);
        }
    }
    private void StartCutScene(string[] textureName)
    {
        cutsceneCam.SetActive(true);
        charCamera.SetActive(false);

        cutsceneCam.GetComponent<Animation>().Play();
        cutSceneStarted = true;
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("StartCutScene", StartCutScene);
    }
}
