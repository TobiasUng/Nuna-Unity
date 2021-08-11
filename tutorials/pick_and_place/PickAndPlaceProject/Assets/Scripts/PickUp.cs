using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public Transform pickUp_pos;
    public bool isPickedUp = false;

    private void Update()
    {
        if (isPickedUp)
        {
            //this.transform.position = pickUp_pos.position;
        }
    }

    private void OnMouseDown()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        this.transform.position = pickUp_pos.position;
        this.transform.parent = pickUp_pos;
        isPickedUp = true;
    } 

    private void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
        isPickedUp = false;
    }
}
