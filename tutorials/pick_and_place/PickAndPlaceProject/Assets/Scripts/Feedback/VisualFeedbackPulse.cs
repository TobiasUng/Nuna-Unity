using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualFeedbackPulse : IPulseFeedback
{

    public bool shouldDisplay = true;
    public float? pulseDuration;
    public float? pulseStep = 0.1f;
    public Image indicator_L;
    public Image indicator_R;
    // Start is called before the first frame update
    
    public override void giveFeedback(float distance, float angle, GameObject placement)
    {
        
        //stereoPan = angle;
        if(angle < 0.07 && angle > -0.07)
        {
            indicator_L.color = new Color(indicator_L.color.r, indicator_L.color.g, indicator_L.color.b, 1);
            indicator_R.color = new Color(indicator_R.color.r, indicator_R.color.g, indicator_R.color.b, 1);
        }

        else if (angle < 0)
        {

            indicator_L.color = new Color(indicator_L.color.r, indicator_L.color.g, indicator_L.color.b, Mathf.Abs(angle));
            indicator_R.color = new Color(indicator_R.color.r, indicator_R.color.g, indicator_R.color.b, 0);
            
        }

        else
        {
            indicator_L.color = new Color(indicator_L.color.r, indicator_L.color.g, indicator_L.color.b, 0);
            indicator_R.color = new Color(indicator_R.color.r, indicator_R.color.g, indicator_R.color.b, Mathf.Abs(angle));
        }
        //audioSource.panStereo = angle;
        calcPulseStep(distance);
        pulsate();
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

        if (Time.time > pulseDuration)
        {
            shouldDisplay = !shouldDisplay;
            pulseDuration = Time.time + pulseStep;
        }

        if (!shouldDisplay)
        {
            indicator_L.color = new Color(indicator_L.color.r, indicator_L.color.g, indicator_L.color.b, 0);
            indicator_R.color = new Color(indicator_R.color.r, indicator_R.color.g, indicator_R.color.b, 0);
        }
    }

    public override void stopFeedback()
    {
        indicator_L.color = new Color(indicator_L.color.r, indicator_L.color.g, indicator_L.color.b, 0);
        indicator_R.color = new Color(indicator_R.color.r, indicator_R.color.g, indicator_R.color.b, 0);
    }
}
