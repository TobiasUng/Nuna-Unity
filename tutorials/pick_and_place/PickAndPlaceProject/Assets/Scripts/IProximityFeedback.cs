using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IProximityFeedback : MonoBehaviour
{

    public abstract void pulsate();
    public abstract void startFeedback(float distance, float angle);
    public abstract void stopFeedback();


}
