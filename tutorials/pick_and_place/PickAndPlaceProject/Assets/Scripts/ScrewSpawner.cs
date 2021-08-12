using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewSpawner : MonoBehaviour
{
   
    public GameObject screwPrefab;
    public GameObject screw;
    public GameObject publisher;
    



    private void Start()
    {
        
    }

    

    public GameObject spawnScrew()
    {
        if(screw == null)
        {
            screw = Instantiate(screwPrefab, transform.position, Quaternion.identity);
            return screw;
        }

        return null;

    }

}
