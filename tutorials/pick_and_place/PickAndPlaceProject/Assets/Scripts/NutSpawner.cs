using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NutSpawner : MonoBehaviour
{
    public GameObject nutPrefab;
    public Slider nut_progress;
    public float time;

    public void spawnNut()
    {
        GameObject nut = Instantiate(nutPrefab);
        
        nut.GetComponent<Renderer>().material = this.GetComponent<Renderer>().material;
        nut.GetComponent<PickUpAndPlaceNut>().nut_progress = nut_progress;
        nut.GetComponent<PickUpAndPlaceNut>().time = time;
        nut.transform.position = transform.position;
        
    }
}
