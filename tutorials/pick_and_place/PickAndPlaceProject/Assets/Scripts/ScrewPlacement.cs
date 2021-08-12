using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPlacement : MonoBehaviour
{

    public GameObject screwSpawner;
    public GameObject screw;
    public GameObject screwRepresentation;
    public GameObject publisher;
    public IFeedback feedback;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        feedback = transform.root.GetComponent<WallConfig>().feedback;
    }

    private void Update()
    {
        if(screw != null) // TODO publisher.GetComponent<TrajectoryPlanner>().isExecuting
        {
            feedback.giveFeedback(getDistance(), Angle.AngleDir(player.transform, screw.transform), gameObject.transform.GetChild(0).gameObject);

            if (!publisher.GetComponent<TrajectoryPlanner>().isExecuting)
            {
                Destroy(screw);
            }
        }

    }

    public void moveScrewToPlacement()
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

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == screw)
        {
            
            GameObject screwRep = Instantiate(screwRepresentation);
            screwRep.transform.parent = this.transform;
            screwRep.transform.position = this.transform.GetChild(0).position;
            Destroy(transform.GetChild(0).gameObject);

            /*if(this.transform.parent.parent.childCount < 2)
            {
                Destroy(this.transform.parent.parent.gameObject);
            }

            else
            {
                Destroy(transform.parent.gameObject);
            }*/
            
            Destroy(screw);
            feedback.stopFeedback();
        }
    }

    private float getDistance()
    {
        return Mathf.Abs(Vector3.Distance(screw.transform.position, transform.position));
    }
}
