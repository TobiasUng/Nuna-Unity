using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IProximityFeedback : MonoBehaviour
{

    public abstract void pulsate();
    public abstract void giveFeedback(float distance, float angle);
    public abstract void stopFeedback();

    public static float mapToZeroOne(float value, float from, float to)
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
