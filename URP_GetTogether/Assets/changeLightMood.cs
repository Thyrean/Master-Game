using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLightMood : MonoBehaviour
{
    public GameObject Core;

    public GameObject[] lights;

    public Color baseColor;
    public Color newColor;

    [ColorUsage(true, true)]
    public Color hdrColor;
    [ColorUsage(true, true)]
    public Color newHDRColor;

    public Color baseLightColor;
    public Color newLightColor;


    public Color extraLightColor;
    public Color newExtraLightColor;

    public GameObject pointLight;
    public GameObject newPointLight;

    public float duration = 10;
    public float t = 1f;

    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        if (this.gameObject.activeSelf == true)
            t = (Time.time - startTime) / duration;

        //t -= 1.0f / 0.1f * Time.deltaTime;

        for (var i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(hdrColor, newHDRColor, t));
        }

        Core.GetComponent<Renderer>().material.SetColor("Color_B3B09809", Color.Lerp(baseColor, newColor, t));

        pointLight.GetComponent<Light>().color = Color.Lerp(baseLightColor, newLightColor, t);
        newPointLight.GetComponent<Light>().color = Color.Lerp(extraLightColor, newExtraLightColor, t);
    }
}
