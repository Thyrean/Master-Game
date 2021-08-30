using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour
{
    public AudioSource mainTheme;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(mainTheme.volume == 0)
            {
                mainTheme.volume = 0.05f;
            }

            else if(mainTheme.volume != 0)
            {
                mainTheme.volume = 0;
            }
        }
    }
}
