using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;


#pragma warning disable IDE1006 // Naming Styles
public class scr1 : MonoBehaviour
#pragma warning restore IDE1006 // Naming Styles
{
    //public float angle;
    //float ratioMultiplier;
    //Vector3 vector;
    
   
    

    
   

    

   
    
}

//public class scr1 : MonoBehaviour
//{

//    float m_Speed;
//    bool m_HitDetect;

//    Collider m_Collider;
//    RaycastHit m_Hit;

//    public Vector3 center;
//    public Vector3 halfExtents;
//    public Vector3 direction;
//    Quaternion orientation;
//    public float maxDistance;
//    public LayerMask ground;
//    public int layerMask;

//    void Start()
//    {
//        //Choose the distance the Box can reach to

//        m_Speed = 0.2f;
//        m_Collider = GetComponent<Collider>();

//        center = transform.position;
//        direction = -transform.up;
//        orientation = transform.rotation;
//        halfExtents = transform.localScale;

//        //maxDistance = 1f;
//    }

//    void Update()
//    {
//        //Simple movement in x and z axes
//        center = transform.position + 2 * transform.up;
//        direction = -transform.up;
//        orientation = transform.rotation;

//        //Test to see if there is a hit using a BoxCast
//        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
//        //Also fetch the hit data
//        m_HitDetect = Physics.BoxCast(center, halfExtents, direction, out m_Hit, orientation, maxDistance, ground);
//        //Physics.BoxCast()
//        if (m_HitDetect)
//        {
//            //Output the name of the Collider your Box hit
//            Debug.Log("Hit : " + m_Hit.collider.name);



//        }

//        float zAxis = 0;
//        if (m_HitDetect)
//        {
//            zAxis = Input.GetAxis("Vertical") * m_Speed;
//        }
//        float yAxis = Input.GetAxis("Horizontal") * m_Speed;

//        transform.Translate(new Vector3(0, yAxis, zAxis));

//    }

//    void FixedUpdate()
//    {


//    }

//    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
//    void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;

//        //Check if there has been a hit yet
//        if (m_HitDetect)
//        {
//            //Draw a Ray forward from GameObject toward the hit
//            Gizmos.DrawRay(center, direction * m_Hit.distance);
//            //Draw a cube that extends to where the hit exists
//            Gizmos.DrawWireCube(center + direction * m_Hit.distance, halfExtents);
//        }
//        //If there hasn't been a hit yet, draw the ray at the maximum distance
//        else
//        {
//            //Draw a Ray forward from GameObject toward the maximum distance
//            Gizmos.DrawRay(center, direction * maxDistance);
//            //Draw a cube at the maximum distance
//            Gizmos.DrawWireCube(center + direction * maxDistance, halfExtents);
//        }
//    }
//}
