using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Assets.Scripts.ActionReactionSystem;


public class symbolRiddle : MonoBehaviour
{
    public Texture[] textures;
    public GameObject[] lights;
    public int index;

    public float syncFillAmount;

    public bool firstTry, secondTry, thirdTry, completed;

    public float changeInterval = 0.33F;
    public Renderer rend;

    public Material Neutral, Wrong, Correct;

    public Image progressBar;

    void Start()
    {
        progressBar.fillAmount = 1f;

        //syncFillAmount = progressBar.fillAmount;

        firstTry = true;
        secondTry = false;
        thirdTry = false;
        completed = false;

        index = 0;

        ReactionManager.Add("ReceiveSymbol", ReceiveSymbol);
        //ReactionManager.Add("UpdateProgressBar", UpdateProgressBar);
        

        rend = GetComponent<Renderer>();

        for (var i = 0; i < lights.Length; i++)
        {
            lights[i].GetComponent<Renderer>().material = Neutral;  
        }
    }

    void Update()
    {
        if (textures.Length == 0)
            return;

        //if(NetworkServer.active)
            //ReactionManager.Call("UpdateProgressBar");
        UpdateProgressBar();

    }
    
    private void ReceiveSymbol(string[] textureName)
    {
        Debug.Log("Script called! TextureName is:" + textureName[0]);

        if (rend.material.mainTexture.name == textureName[0])
        {
            Debug.Log("Correct Texture entered");

            if (thirdTry == true)
            {
                lights[2].GetComponent<Renderer>().sharedMaterial = Correct;
                thirdTry = false;
                completed = true;
            }
            else if (secondTry == true)
            {
                lights[1].GetComponent<Renderer>().sharedMaterial = Correct;
                secondTry = false;
                thirdTry = true;
            }
            else if (firstTry == true)
            {
                lights[0].GetComponent<Renderer>().sharedMaterial = Correct;
                firstTry = false;
                secondTry = true;
            }

        }

        if (rend.material.mainTexture.name != textureName[0])
        {
            Debug.Log("Wrong Texture entered");
            if (thirdTry == true)
            {
                lights[2].GetComponent<Renderer>().sharedMaterial = Wrong;
                thirdTry = false;
                completed = true;
            }
            else if (secondTry == true)
            {
                lights[1].GetComponent<Renderer>().sharedMaterial = Wrong;
                thirdTry = true;
                secondTry = false;
            }
            else if (firstTry == true)
            {
                lights[0].GetComponent<Renderer>().sharedMaterial = Wrong;
                secondTry = true;
                firstTry = false;
            }
        }

        UpdateRiddle();
    }

    private void UpdateRiddle()
    {
        if ((lights[0].GetComponent<Renderer>().sharedMaterial == Wrong || lights[1].GetComponent<Renderer>().sharedMaterial == Wrong ||
                    lights[2].GetComponent<Renderer>().sharedMaterial == Wrong) && completed == true)
        {
            firstTry = true;
            completed = false;

            ReactionManager.Call("AssignFirstMaterials");

            StartCoroutine(resetLights(2f));
        }

        else if (lights[0].GetComponent<Renderer>().sharedMaterial == Correct && lights[1].GetComponent<Renderer>().sharedMaterial == Correct
            && lights[2].GetComponent<Renderer>().sharedMaterial != Wrong)
        {
            ReactionManager.Call("AssignThirdMaterials");
        }

        else if ((lights[0].GetComponent<Renderer>().sharedMaterial == Correct || (lights[0].GetComponent<Renderer>().sharedMaterial == Wrong &&
            lights[1].GetComponent<Renderer>().sharedMaterial == Correct)) && lights[2].GetComponent<Renderer>().sharedMaterial != Wrong)
        {
            ReactionManager.Call("AssignSecondMaterials");
        }

        if (lights[0].GetComponent<Renderer>().sharedMaterial == Correct && lights[1].GetComponent<Renderer>().sharedMaterial == Correct &&
            lights[2].GetComponent<Renderer>().sharedMaterial == Correct)
        {
            Debug.Log("YAY!");
            gameObject.SetActive(false);
        }
    }

    private void UpdateProgressBar()
    {
        //index = Mathf.FloorToInt(Time.time / changeInterval);
        //index = index % textures.Length;
        //rend.material.mainTexture = textures[index];

        progressBar.fillAmount -= 1.0f / changeInterval * Time.deltaTime;

        if (progressBar.fillAmount == 0)
        {
            progressBar.fillAmount = 1f;

            if (index < 2)
            {
                index = index + 1;
            }
            else if(index == 2)
            {
                index = 0;
            }
        }

        rend.material.mainTexture = textures[index];
    }

    private IEnumerator resetLights(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        lights[0].GetComponent<Renderer>().sharedMaterial = Neutral;
        lights[1].GetComponent<Renderer>().sharedMaterial = Neutral;
        lights[2].GetComponent<Renderer>().sharedMaterial = Neutral;
    }
}
