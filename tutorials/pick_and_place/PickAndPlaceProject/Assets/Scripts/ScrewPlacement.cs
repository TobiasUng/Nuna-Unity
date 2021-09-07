using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScrewPlacement : MonoBehaviour
{

    public GameObject screwSpawner;
    public GameObject screw;
    public GameObject screwRepresentation;
    public GameObject publisher;
    public IFeedback[] feedbacks;
    public GameObject playerCamera;

    private Material screwColor;

    public UnityEvent onDestroy;


    // Start is called before the first frame update
    void Start()
    {
        if(feedbacks.Length == 0)
        {
            feedbacks = transform.root.GetComponent<WallConfig>().feedbacks;
        }
        
        playerCamera = transform.root.GetComponent<WallConfig>().playerCamera;
        publisher = transform.root.GetComponent<WallConfig>().publisher;
        screwSpawner = transform.root.GetComponent<WallConfig>().screwSpawner;
        screwColor = transform.root.GetComponent<WallConfig>().randomColor();
    }

    private void Update()
    {
        if(screw != null) // TODO publisher.GetComponent<TrajectoryPlanner>().isExecuting
        {
            foreach(IFeedback feedback in feedbacks)
            {
                feedback.giveFeedback(getDistance(), Angle.AngleDir(playerCamera.transform, this.transform.GetChild(0).transform), gameObject.transform.GetChild(0).gameObject);
            }
            

            if (!publisher.GetComponent<TrajectoryPlanner>().isExecuting)
            {
                Destroy(screw);
            }
        }

    }

    public void moveScrewToPlacement()
    {
        if(screwSpawner.GetComponent<ScrewSpawner>().screw == null && !publisher.GetComponent<TrajectoryPlanner>().isExecuting)
        {
            spawnScrewAsChild();
            var trajectoryPlanner = publisher.GetComponent<TrajectoryPlanner>();
            
            trajectoryPlanner.target = screw;
            trajectoryPlanner.targetPlacement = this.gameObject;
            trajectoryPlanner.PublishJoints();
            
        }
    }

    public void spawnScrewAsChild()
    {
        
        screw = screwSpawner.GetComponent<ScrewSpawner>().spawnScrew();
        
        
        screw.GetComponent<Renderer>().material = gameObject.GetComponent<Renderer>().material;
        screw.transform.parent = gameObject.transform.parent;
        
        
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject == screw)
        {
            
            GameObject screwRep = Instantiate(screwRepresentation);
            screwRep.GetComponent<Renderer>().material = screwColor;
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

            foreach (IFeedback feedback in feedbacks)
            {
                feedback.stopFeedback();
            }
            

            onDestroy.Invoke();
        }
    }

    private float getDistance()
    {
        return Mathf.Abs(Vector3.Distance(screw.transform.position, transform.position));
    }
}
