using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLead : MonoBehaviour
{
    public GameObject wall;
    public GameObject screwPositions;
    //Transform[] rows;
    public SortedDictionary<string, Transform> screwPlacementsMapRobot = new SortedDictionary<string, Transform>();
    public SortedDictionary<string, Transform> screwPositionsMapOperator = new SortedDictionary<string, Transform>();
    public List<Transform> screwPlacementsRobot = new List<Transform>();
    public List<Transform> screwPositionsOperator = new List<Transform>();
    public Stack<int> operationSequence = new Stack<int>();
    public GameObject publisher;
    public bool hasStarted = false;
    
    // Start is called before the first frame update
    void Start()
    {

        /*wallSections = wall.GetComponentsInChildren<Transform>();
        Debug.Log(wallSections.Length);*/

        if (screwPositions != null)
        {
            foreach (Transform row in screwPositions.transform)
            {
                foreach (Transform placement in row.transform)
                {
                    screwPositionsOperator.Add(placement);
                }
            }
        }

        

        foreach (Transform row in wall.transform)
        {
            foreach(Transform placement in row.transform)
            {
                if (screwPositions != null)
                {
                    int i = Random.Range(0, screwPositionsOperator.Count);
                    Transform randomScrewPosition = screwPositionsOperator[i];
                    screwPositionsOperator.RemoveAt(i);
                    randomScrewPosition.parent = placement.GetChild(0);
                }

                screwPlacementsRobot.Add(placement);
                screwPlacementsMapRobot.Add(row.tag + placement.GetSiblingIndex(), placement);
            }
        }

        

        operationSequence = getOperationSequence();
    }

    // Update is called once per frame
    void Update()
    {
        
         if (!publisher.GetComponent<TrajectoryPlanner>().isExecuting && operationSequence.Count > 0 && hasStarted)
           {
                int i = operationSequence.Pop();
                screwPlacementsRobot[i].GetChild(0).GetComponent<ScrewPlacement>().moveScrewToPlacement();
           }
        
        
    }

    public void startOperation()
    {
        hasStarted = true;
    }

    private Stack<int> getOperationSequence()
    {
        List<int> placementIndices = new List<int>();
        for(int i = 0; i < screwPlacementsRobot.Count; i++)
        {
            placementIndices.Add(i);
        }


        Stack<int> sequence = new Stack<int>();
        
        while(placementIndices.Count > 0)
        {
            int i = Random.Range(0, placementIndices.Count);
            sequence.Push(placementIndices[i]);
            placementIndices.RemoveAt(i);
        }

       

        return sequence;
    }
}
