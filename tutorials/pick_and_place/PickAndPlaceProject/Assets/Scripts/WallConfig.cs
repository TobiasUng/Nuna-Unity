using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallConfig : MonoBehaviour
{
    public IFeedback feedback;
    public GameObject playerCamera;
    public GameObject publisher;
    public GameObject screwSpawner;
    public Material[] screwColors;

    public SortedDictionary<string, Transform> screwPlacementsMap = new SortedDictionary<string, Transform>();
    public List<Transform> screwPlacements = new List<Transform>();

    private void Start()
    {
        checkScrews();
    }

    public Material randomColor()
    {
        return screwColors[Random.Range(0, screwColors.Length)];
    }

    public void checkScrews()
    {

        screwPlacementsMap.Clear();
        screwPlacements.Clear();

        foreach (Transform row in this.transform)
        {
            foreach (Transform placement in row.transform)
            {
                screwPlacements.Add(placement);
                
                screwPlacementsMap.Add(row.tag + placement.GetSiblingIndex(), placement);
            }
        }

        Debug.Log(screwPlacements.Count);

        if (screwPlacements.Count == 0)
        {
            Destroy(this.gameObject);
        }
    }


}
