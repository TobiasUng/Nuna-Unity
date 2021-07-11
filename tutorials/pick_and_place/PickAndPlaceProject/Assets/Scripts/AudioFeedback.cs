using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioFeedback : IProximityFeedback
{

    public AudioSource audioSource;
    public bool shouldPlay = true;
    public float? pulseDuration;
    public float? pulseStep = 0.05f;

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

    
    public override void startFeedback(float distance, float angle)
    {

       
    }

    public override void stopFeedback()
    {

    }
    /*public void calcPulseStep(float distance)
    {
        pulseStep = Mathf.Abs(mapToZeroOne(distance, 0, 1) - 1);
    }*/

    public override void pulsate()
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
