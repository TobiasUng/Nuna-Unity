using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutSpawner : MonoBehaviour
{
    public GameObject nutPrefab;
    public Transform pickUp_pos;
    public Slider nut_progress;

    private void OnMouseDown()
    {
        GameObject nut = Instantiate(nutPrefab);
        nut.GetComponent<Rigidbody>().useGravity = false;
        nut.GetComponent<Rigidbody>().isKinematic = true;
        nut.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
        nut.GetComponent<PickUpAndPlaceNut>().pickUp_pos = pickUp_pos;
        nut.GetComponent<PickUpAndPlaceNut>().nut_progress = nut_progress;
        nut.transform.position = pickUp_pos.position;
        nut.transform.parent = pickUp_pos;
    }
}
