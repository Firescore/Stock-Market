using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbStair : MonoBehaviour
{
    private Rigidbody rigidbody;
    [SerializeField] GameObject stepRayUpper;
    [SerializeField] GameObject stepRayDown;
    [SerializeField] float stepHeight = 0.1f;
    [SerializeField] float stepSmooth = 0.1f;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        stepRayUpper.transform.position = new Vector3(stepRayUpper.transform.position.x, stepHeight, stepRayUpper.transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stepClimb();
    }
    void stepClimb()
    {
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayDown.transform.position, transform.TransformDirection(Vector3.forward), out hitLower, 0.02f)){

            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.transform.position, transform.TransformDirection(Vector3.forward), out hitUpper, 0.1f)){
                rigidbody.position -= new Vector3(0, -stepSmooth, 0);
            }
        }
    }
}
