using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPlacement : MonoBehaviour
{

    public GameObject screwSpawner;
    public GameObject screw;
    public GameObject publisher;
    public IProximityFeedback feedback;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(screw != null)
        {
            feedback.giveFeedback(getDistance(), Angle.AngleDir(player.transform, screw.transform));
        }

    }

    public void moveTargetToPlacement()
    {
        spawnScrewAsChild();
        var trajectoryPlanner = publisher.GetComponent<TrajectoryPlanner>();
        if (screw != null)
        {
            trajectoryPlanner.target = screw;
            trajectoryPlanner.targetPlacement = this.gameObject;
            trajectoryPlanner.PublishJoints();
        }
        

    }

    public void spawnScrewAsChild()
    {
        screw = screwSpawner.GetComponent<ScrewSpawner>().spawnScrew();
        if(screw != null)
        {
            screw.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
            screw.transform.parent = gameObject.transform.parent;
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == screw)
        {
            //transform.GetChild(0).GetComponent<Renderer>().material = Resources.Load("Materials/Green", typeof(Material)) as Material; 
            
            Destroy(transform.GetChild(0).gameObject);
            Destroy(screw);
            feedback.stopFeedback();
        }
    }

    private float getDistance()
    {
        return Mathf.Abs(Vector3.Distance(screw.transform.position, transform.position));
    }
}
