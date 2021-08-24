using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Assets.Scripts.ActionReactionSystem;

public class ChangeLights : MonoBehaviour
{
    public GameObject Core;

    public GameObject[] lights;

    public Color baseColor;
    public Color newColor;

    public Color baseLightColor;
    public Color newLightColor;

    public Color lerpLightColor;

    public GameObject pointLight;
    public GameObject newPointLight;

    public float duration = 10;
    public float t = .2f;

    public bool gameFinished;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        gameFinished = false;

        ReactionManager.Add("ChangeColors", ChangeColors);
        ReactionManager.Add("GameFinished", GameFinished);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) { 
            ReactionManager.Call("GameFinished");
            ReactionManager.Call("StartCutScene");
        }

        if (gameFinished == true)
        {
            ReactionManager.Call("ChangeColors");
        }

    }

    private void ChangeColors(string[] textureName)
    {
        t = (Time.time - startTime) * .2f;

        for (var i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(baseColor, newColor, t * Time.deltaTime));
        }

        Core.GetComponent<Renderer>().material.SetColor("Color_B3B09809", Color.Lerp(baseColor, newColor, t * Time.deltaTime));

        pointLight.GetComponent<Light>().color = Color.Lerp(baseLightColor, newLightColor, t * Time.deltaTime);
        newPointLight.SetActive(true);
    }

    private void GameFinished(string[] textureName)
    {
        gameFinished = true;
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("ChangeColors", ChangeColors);
        ReactionManager.Remove("GameFinished", GameFinished);
    }
}
