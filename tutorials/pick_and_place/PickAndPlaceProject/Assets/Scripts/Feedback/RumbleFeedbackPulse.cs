using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleFeedbackPulse : IPulseFeedback
{
    public float minMotorSpeed = 0.1f;
    public bool shouldRumble = true;
    public float? pulseDuration;
    public float? pulseStep = 0.1f;

    [Header("Low Frequency Rumble")]
    [Range(0.0f, 1.0f)]
    public float loFreq = 0.2f;
    [Header("High Frequency Rumble")]
    [Range(0.0f, 1.0f)]
    public float hiFreq = 0.1f;

    private void Update()
    {
        
    }

    /*public void startRumble(float distance)
    {
        if (Gamepad.current != null)
        {
            loFreq = hiFreq = Mathf.Max(mapToZeroOne(distance, 0, 1), minMotorSpeed);
            Gamepad.current.SetMotorSpeeds(0.0f, hiFreq);
        }
    }*/

    public override void giveFeedback(float distance, float angle, GameObject placement)
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
    }

    public override void pulsate()
    {
        if (pulseDuration == null)
        {
            pulseDuration = Time.time + pulseStep;
        }

        if(Time.time > pulseDuration)
        {
            shouldRumble = !shouldRumble;
            pulseDuration = Time.time + pulseStep;

            if (shouldRumble)
            {
                Gamepad.current.SetMotorSpeeds(0, hiFreq);
            }

            else
            {
                Gamepad.current.SetMotorSpeeds(0, 0);
            }

        }
        
    }

    public override void stopFeedback()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
        }
    }

   

}
