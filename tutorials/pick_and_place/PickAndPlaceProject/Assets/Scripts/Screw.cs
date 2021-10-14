using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Screw : MonoBehaviour
{

    public static List<float> screwDistances = new List<float>();
    public UnityEvent onStart;
    public float distanceToPlayer;
    public float dangerDistance = 0.4f;
    public float duration = 8f;
    public float time = 0f;
    public GameObject nut = null;
    public bool isFixed = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("VR Camera");
        distanceToPlayer = Vector3.Distance(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        PlayerStats.pilotStats.screwsPlaced++;
        PlayerStats.pilotStats.completionTime = Time.realtimeSinceStartup - PlayerStats.startTime;

        screwDistances.Add(distanceToPlayer);

        PlayerStats.pilotStats.avarageScrewDistance = System.Linq.Enumerable.Average(screwDistances);


        if (distanceToPlayer <= dangerDistance)
        {
            PlayerStats.pilotStats.dangerErrors++;
            GetComponent<AudioSource>().Play();
        }
        onStart.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > duration && !isFixed)
        {
            if(nut != null)
            {
                nut.SetActive(false);
                //Destroy(nut);
                nut.GetComponent<PickUpAndPlaceNut>().resetProgress();
            }
            Destroy(this.gameObject);
            PlayerStats.pilotStats.errors++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("nut"))
        {
            if (this.GetComponent<Renderer>().material.color == other.GetComponent<Renderer>().material.color)
            {
                nut = other.gameObject;
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        nut = null;
    }
}
