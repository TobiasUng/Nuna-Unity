using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Screw : MonoBehaviour
{

    public UnityEvent onStart;
    public float distanceToPlayer;
    public float dangerDistance = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("VR Camera");
        distanceToPlayer = Vector3.Distance(transform.position, new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        if(distanceToPlayer <= dangerDistance)
        {
            GetComponent<AudioSource>().Play();
        }
        onStart.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if(other.gameObject.tag == "Player")
        {
            GetComponent<AudioSource>().Play();
        }*/
    }
}
