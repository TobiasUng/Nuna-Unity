using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFeedbackContinuous: IContinuousFeedback
{
    public GameObject circlePrefab;
    GameObject circleIndicator;
    

    // Start is called before the first frame update
    public override void giveFeedback(float distance, float angle, GameObject placement)
    {
        if (circleIndicator == null)
        {
            circleIndicator = Instantiate(circlePrefab, placement.transform);
            //circleIndicator.transform.parent = placement.transform;
        }
        continuous(distance, placement);
    }

    public override void continuous(float distance, GameObject placement)
    {
        Vector3 placementScale = placement.transform.localScale;
        circleIndicator.transform.localScale = new Vector3(distance * 10 + placementScale.x, 0.1f, distance * 10 + placementScale.z);
    }

    public override void stopFeedback()
    {
        Destroy(circleIndicator);
    }
}
