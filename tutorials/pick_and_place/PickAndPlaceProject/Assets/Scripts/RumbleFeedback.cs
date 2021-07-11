using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleFeedback : MonoBehaviour
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

    public void startRumble(float distance, float angle)
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

    public void pulsate()
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

    public void stopRumble()
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0.0f, 0.0f);
        }
    }

    private float mapToZeroOne(float value, float from, float to)
    {
        float mappedValue = (value - from) / (to - from) * (1f - 0) + 0;
        mappedValue = Mathf.Abs(mappedValue - 1);
        return mappedValue;
        return Mathf.Pow(mappedValue, 2);
        //return Mathf.Pow(mappedValue, 6);
        /*
        Debug.Log( (float)(maxMotorSpeed * Mathf.Pow(mappedValue, 4)));
        return (float) (maxMotorSpeed * Mathf.Pow(mappedValue, 4));*/
    }

}
