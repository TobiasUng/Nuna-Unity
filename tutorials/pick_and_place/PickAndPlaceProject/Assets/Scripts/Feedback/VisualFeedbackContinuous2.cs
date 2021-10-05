using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFeedbackContinuous2 : IContinuousFeedback
{
    public GameObject barPrefab;
    GameObject barIndicator;


    // Start is called before the first frame update
    public override void giveFeedback(float distance, float angle, GameObject placement)
    {
        if (barIndicator == null)
        {
            //circleIndicator = Instantiate(circlePrefab, placement.transform);
            barIndicator = Instantiate(barPrefab);
            barIndicator.transform.position = placement.transform.position;
            //circleIndicator.transform.parent = placement.transform;
        }
        continuous(distance, placement);
    }

    public override void continuous(float distance, GameObject placement)
    {
        Vector3 placementScale = placement.transform.localScale;
        //circleIndicator.transform.localScale = new Vector3(distance + placementScale.x, 0.1f, distance  + placementScale.z);
        //barIndicator.transform.localScale = new Vector3(distance / 2, 0.1f, distance / 2);
        barIndicator.transform.localScale = new Vector3(distance / 2, barIndicator.transform.localScale.y, barIndicator.transform.localScale.z);
    }

    public override void stopFeedback()
    {
        Destroy(barIndicator);
    }
}
