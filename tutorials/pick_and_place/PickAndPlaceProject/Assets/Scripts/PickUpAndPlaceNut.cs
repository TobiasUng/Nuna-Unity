using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpAndPlaceNut : MonoBehaviour
{
    public Transform pickUp_pos;
    public Slider nut_progress;
    private float sliderProgress = 0;
    private bool isPickUp = true;

    

    private void OnMouseDown()
    {
        if (isPickUp)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
            this.transform.position = pickUp_pos.position;
            this.transform.parent = pickUp_pos;
        }
        
        
    } 

    private void OnMouseUp()
    {
        if(isPickUp)
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().isKinematic = false;
        }
        
    }



    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("screw") && isPickUp)
        {
            if (this.GetComponent<Renderer>().material.color == other.GetComponent<Renderer>().material.color && other.transform.childCount == 0)
            {
                sliderProgress += Time.deltaTime / 3f;
                nut_progress.value = sliderProgress;
            }                      
        }

        if (sliderProgress >= 1)
        {
            isPickUp = false;
            resetProgress();
            
            GameObject screw = other.transform.gameObject;

     
            this.transform.parent = null;
            this.transform.position = screw.transform.position;
            this.transform.rotation = Quaternion.identity;
            this.transform.parent = screw.transform;

            Transform screwPlacement = screw.transform.parent.parent;
            screw.transform.parent = null;
            Transform row = screwPlacement.parent;
            Destroy(screwPlacement.gameObject);
            if (row.childCount == 0)
            {
                Destroy(row.gameObject);
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("screw") && isPickUp)
        {
            resetProgress();
        }
    }

    private void resetProgress()
    {
        sliderProgress = 0;
        nut_progress.value = sliderProgress;

    }


}
