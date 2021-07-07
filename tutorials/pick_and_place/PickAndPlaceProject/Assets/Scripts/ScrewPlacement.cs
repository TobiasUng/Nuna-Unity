using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewPlacement : MonoBehaviour
{

    public GameObject screwSpawner;
    public GameObject screw;
    public GameObject publisher;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(screw != null)
        {
            Debug.Log(getDistance());
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
            Destroy(screw);
        }
    }

    private float getDistance()
    {
        return Mathf.Abs(Vector3.Distance(screw.transform.position, transform.position));
    }
}
