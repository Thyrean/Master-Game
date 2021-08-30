using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hdrColorImage : MonoBehaviour
{
    // Start is called before the first frame update
    public Image image;

    [ColorUsageAttribute(true, true)]
    public Color hdrColor;

    void Start()
    {
        image.color = hdrColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
