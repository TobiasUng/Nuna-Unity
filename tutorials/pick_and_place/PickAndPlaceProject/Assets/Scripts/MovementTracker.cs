using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTracker : MonoBehaviour
{

    public Vector3 playerPosPrevious;
    public float distanceMoved;
    // Start is called before the first frame update
    void Start()
    {
        playerPosPrevious = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceMoved += Vector3.Distance(transform.position, playerPosPrevious);
        playerPosPrevious = transform.position;
        PlayerStats.pilotStats.distanceMoved = distanceMoved;
    }
}
