using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPulseFeedback : IFeedback
{
    public readonly float deadZone = 0.07f;
    public abstract void pulsate();
    
}
