using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public GameObject publisher;
    public Transform playerPosCurrent;
    public Vector3 playerPosPrevious;
    public float robotIdleTime = 0f;
    public float distanceMoved = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerPosPrevious = playerPosCurrent.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceMoved += Vector3.Distance(playerPosCurrent.position, playerPosPrevious);
        playerPosPrevious = playerPosCurrent.position;

    }
}
