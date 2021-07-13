using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IContinuousFeedback : IFeedback
{
    public abstract void continuous(float distance, GameObject placement);
}
