using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform Pointer;
    RaycastHit hit;
    public LayerMask Ignoring;
    public float RayL;
    

    void FixedUpdate()
    {

        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out hit, RayL, ~Ignoring))
        {
            Pointer.position = hit.point;
            
        }
        else
        {
            Pointer.position = ray.origin + ray.direction * RayL;
        }




    }

}
/* public class CameraFollow : MonoBehaviour
{
    public Transform ObjectToFollow;
    public Transform PointToFollow;
    private Vector3 offset;
    public Transform Pointer;
    RaycastHit hit;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.2f;

    public bool RotateAround;
    public float RotSpeed = 3.0f;


    private void Start()
    {
        
        
        offset = transform.position - ObjectToFollow.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {


        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotSpeed, Vector3.up);
        offset = camTurnAngle * offset;
        Quaternion camTurnAngle2 = Quaternion.AngleAxis(Input.GetAxis("Mouse Y"), Vector3.left);
        offset = camTurnAngle2 * offset;
        Vector3 newPos = ObjectToFollow.position + offset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);    
        transform.LookAt(PointToFollow);
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            Pointer.position = hit.point;
               }

        
        
    }
} */
