using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFeedbackContinuous : IContinuousFeedback
{

    public AudioSource audioSource;
    
    

    public override void giveFeedback(float distance, float angle, GameObject placement)
    {

        if(initDistance == null)
        {
            initDistance = distance;
        }

        audioSource.panStereo = angle;

        float adjustment = Mathf.Abs(mapToZeroOne(Mathf.Min(1, distance), 0, (float)initDistance));

        //audioSource.volume = adjustment; //TODO
        audioSource.pitch = 1 +  2 * Mathf.Pow(adjustment, 6); //TODO
        continuous(distance, placement);       
    }

    public override void continuous(float distance, GameObject placement)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    
    public override void stopFeedback()
    {
        initDistance = null;
        audioSource.Stop();
        
    }
}
