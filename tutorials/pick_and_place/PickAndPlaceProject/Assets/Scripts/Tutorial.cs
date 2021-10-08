using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public Color color;
    public float fadeSpeed = 1f;
    public bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {
        color = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFading)
        {
            color = GetComponent<Renderer>().material.color;
            float transparency = color.a;

            if (transparency < 1)
            {
                transparency += fadeSpeed * Time.deltaTime;
                GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, transparency);
            }
        }
        

        
    }

    public void startFading()
    {
        isFading = true;
    }

}
