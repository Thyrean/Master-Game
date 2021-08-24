using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using Assets.Scripts.ActionReactionSystem;

public class assignSymbolRiddle : MonoBehaviour
{
    public Texture[] textures;
    public Texture[] texturesSecond;
    public Texture[] texturesThird;

    public GameObject[] screens;

    // Start is called before the first frame update
    void Start()
    {
        ReactionManager.Add("AssignFirstMaterials", AssignFirstMaterials);
        ReactionManager.Add("AssignSecondMaterials", AssignSecondMaterials);
        ReactionManager.Add("AssignThirdMaterials", AssignThirdMaterials);

        ReactionManager.Call("AssignFirstMaterials");

        for (var i = 0; i < screens.Length; i++)
        {
            screens[i].SetActive(false);
        }
    }

    private void AssignFirstMaterials(string[] textureName)
    {
        for (var i = 0; i < textures.Length; i++)
        {
            screens[i].GetComponent<Renderer>().material.mainTexture = textures[i];
            screens[i].GetComponent<Renderer>().material.SetTexture("_EmissionMap", textures[i]);
            screens[i].GetComponent<clickSymbolScreen>().textureName = textures[i].name;
        }
    }
    private void AssignSecondMaterials(string[] textureName)
    {
        for (var i = 0; i < textures.Length; i++)
        {
            screens[i].GetComponent<Renderer>().material.mainTexture = texturesSecond[i];
            screens[i].GetComponent<Renderer>().material.SetTexture("_EmissionMap", texturesSecond[i]);
            screens[i].GetComponent<clickSymbolScreen>().textureName = texturesSecond[i].name;
        }
    }
    private void AssignThirdMaterials(string[] textureName)
    {
        for (var i = 0; i < textures.Length; i++)
        {
            screens[i].GetComponent<Renderer>().material.mainTexture = texturesThird[i];
            screens[i].GetComponent<Renderer>().material.SetTexture("_EmissionMap", texturesThird[i]);
            screens[i].GetComponent<clickSymbolScreen>().textureName = texturesThird[i].name;
        }
    }

    private void OnDestroy()
    {
        ReactionManager.Remove("AssignFirstMaterials", AssignFirstMaterials);
        ReactionManager.Remove("AssignSecondMaterials", AssignSecondMaterials);
        ReactionManager.Remove("AssignThirdMaterials", AssignThirdMaterials);
    }
}
