using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ButtonPress : MonoBehaviour
{
    private Ray ray;
    public LayerMask buttonLayerMask;
    
    //public Material hover;
    //public Material prevHover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 10f, buttonLayerMask))
        {
            /*prevHover = button.GetComponent<Renderer>().material;
            button.GetComponent<Renderer>().material = hover;*/

            if (Input.GetKeyDown(KeyCode.E))
            {
                
                GameObject button = hit.transform.gameObject;
                //button.GetComponent<TrajectoryPlanner>().PublishJoints();*/
                var screwPlacement = button.GetComponentInParent<ScrewPlacement>();
                screwPlacement.moveTargetToPlacement();
                
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log(other.gameObject.layer);
    
        if(other.gameObject.layer == LayerMask.NameToLayer("button"))
        {
            GameObject button = other.transform.gameObject;
            //button.GetComponent<TrajectoryPlanner>().PublishJoints();*/
            var screwPlacement = button.GetComponentInParent<ScrewPlacement>();
            screwPlacement.moveTargetToPlacement();
        }
    }

   
}
