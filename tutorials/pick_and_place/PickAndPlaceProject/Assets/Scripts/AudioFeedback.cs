using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioFeedback : MonoBehaviour
{

    public AudioSource audioSource;
    public bool shouldPlay = true;
    public float? pulseDuration;
    public float? pulseStep = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        pulseDuration = Time.time + pulseStep;
    }

    // Update is called once per frame
    void Update()
    {
        pulsate();
    }


    /*public void startRumble(float distance, float angle)
    {

        hiFreq = Mathf.Max(0, angle);

        if (Gamepad.current != null)
        {

            calcPulseStep(distance);
            pulsate();
        }
    }

    public void calcPulseStep(float distance)
    {
        pulseStep = Mathf.Abs(mapToZeroOne(distance, 0, 1) - 1);
    }*/

    public void pulsate()
    {
        
        if(Time.time < pulseDuration && shouldPlay)
        {
            shouldPlay = false;
            audioSource.Play();
        }


        if (Time.time > pulseDuration)
        {
            shouldPlay = true;
            pulseDuration = Time.time + pulseStep;
        }

    }
}
