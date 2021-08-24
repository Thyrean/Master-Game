using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public AudioSource walking;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("StableGrounded"))
        {
            walking.Play();
        }
    }
}
