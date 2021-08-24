using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.ActionReactionSystem;
using Lightbug.CharacterControllerPro.Core;

using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class SingleTeleporterPad : MonoBehaviour
{
    public GameObject otherPad;
    public ParticleSystem teleportVfx;

    public Image progressBar;
    public float teleportTimer = 5F;
    public Text infoText;
    public AudioSource teleportSFX;

    public GameObject currentPlayer;

    void Start()
    {
        infoText.enabled = false;
        progressBar.fillAmount = 1.0f;
        progressBar.enabled = false;
    }
    void Update()
    {
        if (progressBar.enabled == true)
        {
            UpdateProgressBar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            teleportVfx.Play();
            teleportSFX.Play();
            currentPlayer = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Character")
        {
            progressBar.enabled = true;
            infoText.enabled = true;

            //StartCoroutine(TeleportPlayer(teleportTimer));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
        {
            currentPlayer = null;
            StopCoroutine(TeleportPlayer(0));
            progressBar.enabled = false;
            infoText.enabled = false;

            progressBar.fillAmount = 1f;
            teleportVfx.Stop();
            teleportSFX.Stop();
        }
       
    }

    IEnumerator TeleportPlayer(float tpTime)
    {
        yield return new WaitForSeconds(tpTime);

        if (currentPlayer == null)
            Debug.Log("No player found!");

        else if(currentPlayer != null)
        {
            Debug.Log("Teleporting player");

            currentPlayer.GetComponent<CharacterActor>().Teleport(otherPad.transform.position + new Vector3(0f, 0f, 2f)
                , otherPad.transform.rotation);
        }
    }

    private void UpdateProgressBar()
    {
        //index = Mathf.FloorToInt(Time.time / changeInterval);
        //index = index % textures.Length;
        //rend.material.mainTexture = textures[index];

        progressBar.fillAmount -= 1.0f / teleportTimer * Time.deltaTime;

        if (progressBar.fillAmount == 0)
        {
            progressBar.fillAmount = 1f;
            StartCoroutine(TeleportPlayer(0));
        }
    }
}
