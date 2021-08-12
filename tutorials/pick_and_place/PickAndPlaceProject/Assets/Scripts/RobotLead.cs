using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotLead : MonoBehaviour
{
    public GameObject wall;
    //Transform[] rows;
    public SortedDictionary<string, Transform> screwPlacementsMap = new SortedDictionary<string, Transform>();
    public List<Transform> screwPlacements = new List<Transform>();
    public Stack<int> operationSequence = new Stack<int>();
    public GameObject publisher;
    
    // Start is called before the first frame update
    void Start()
    {
        
        /*wallSections = wall.GetComponentsInChildren<Transform>();
        Debug.Log(wallSections.Length);*/
        foreach(Transform row in wall.transform)
        {
            foreach(Transform placement in row.transform)
            {
                screwPlacements.Add(placement);
                screwPlacementsMap.Add(row.tag + placement.GetSiblingIndex(), placement);
            }
        }

        operationSequence = getOperationSequence();
    }

    // Update is called once per frame
    void Update()
    {
        if (!publisher.GetComponent<TrajectoryPlanner>().isExecuting && operationSequence.Count > 0)
        {
            int i = operationSequence.Pop();
            screwPlacements[i].GetChild(0).GetComponent<ScrewPlacement>().moveScrewToPlacement();
        }
    }


    private Stack<int> getOperationSequence()
    {
        List<int> placementIndices = new List<int>();
        for(int i = 0; i < screwPlacements.Count; i++)
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
