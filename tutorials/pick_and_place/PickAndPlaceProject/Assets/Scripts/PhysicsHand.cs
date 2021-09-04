using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsHand : MonoBehaviour
{

    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 100f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;

    private Transform followTarget;
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        
        followTarget = followObject.transform;
        body = GetComponent<Rigidbody>();
        body.collisionDetectionMode = CollisionDetectionMode.Continuous;
        body.interpolation = RigidbodyInterpolation.Interpolate;
        body.mass = 20f;

        body.position = followTarget.position;
        body.rotation = followTarget.rotation;

        
    }

    // Update is called once per frame
    void Update()
    {
        PhysicsMove();
        //body.position = followTarget.position;
        //body.rotation = followTarget.rotation;
        //
    }

    private void PhysicsMove()
    {

        var positionWithOffset = followTarget.position + positionOffset;

        var distance = Vector3.Distance(positionWithOffset, transform.position);
        body.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);

        var rotationWithOffset = followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(body.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        body.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
    }

    public void makeTransparent()
    {
        Color color = this.GetComponent<Renderer>().material.color;
        this.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 0.5f);
    }

    public void makeOpaque()
    {
        Color color = this.GetComponent<Renderer>().material.color;
        this.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, 1f);
    }
}
