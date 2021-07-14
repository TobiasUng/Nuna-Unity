using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleFeedbackContinuous : IContinuousFeedback
{

    [Range(0.0f, 1.0f)]
    public float loFreq = 0.2f;
    [Header("High Frequency Rumble")]
    [Range(0.0f, 1.0f)]
    public float hiFreq = 0.1f;

    public override void giveFeedback(float distance, float angle, GameObject placement)
    {

        if (initDistance == null)
        {
            initDistance = distance;
        }

        if (Gamepad.current != null)
        {
            hiFreq = Mathf.Abs(mapToZeroOne(Mathf.Min(1, distance), 0, (float)initDistance)*angle);
            continuous(distance, placement);
            
        }

    }

    public override void continuous(float distance, GameObject placement)
    {
        //hiFreq = Mathf.Min(1, distance);
        
        hiFreq = Mathf.Pow(hiFreq, 3);
        Gamepad.current.SetMotorSpeeds(Mathf.Max(hiFreq, 0.1f), Mathf.Max(hiFreq, 0.1f));
    }


    public override void stopFeedback()
    {
        initDistance = null;
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
}
