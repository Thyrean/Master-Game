using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.CharacterControllerPro.Core;
using Lightbug.CharacterControllerPro.Implementation;
using Lightbug.Utilities;
using System;

public class DeathVFX : MonoBehaviour
{
    [SerializeField] ParticleSystem deathVfx;
    [SerializeField] CharacterActor characterActor;
    [SerializeField] Transform playerTransform;
    [SerializeField] AudioSource recallSFX;

    private Vector3 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        if (characterActor == null)
            return;

        characterActor.OnTeleport += OnTeleport;
    }

    void OnTeleport(Vector3 position, Quaternion rotation)
    {
        //throw new NotImplementedException("The requested feature is not implemented.");

        currentPos = playerTransform.position;
        deathVfx.transform.position = currentPos + new Vector3(0, 1f, 0);
        deathVfx.Play();
        recallSFX.Play();
    }
}
