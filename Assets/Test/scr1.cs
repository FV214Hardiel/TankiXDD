using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.InputSystem;


public class scr1 : MonoBehaviour
{
    public float angle;
    float ratioMultiplier;
    Vector3 vector;
    
    List<float> angles;
    int index;
    public GameObject gameObject111;
    private void Start()
    {
        angles = new List<float>();
        for (int _ = 0; _ < 20; _++)
        {
            angles.Add(Random.Range(2, 356));
        }
        //transform.position = Random.insideUnitCircle * 2;
        vector = transform.forward*10;
        index = 0;

        //From given accuracy calculating final tan of vector
        angle = angle * Mathf.Deg2Rad;
        ratioMultiplier = Mathf.Atan(angle);
        
        
    }

    void FixedUpdate()
    {
        
        //Debug.Log(angles[index]);
        


       
    }
    
    void Update()
    {
        
        Debug.DrawRay(transform.position, vector, Color.red);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Making velocity vector as sum of forward + up dispersion 
            vector = transform.forward + Quaternion.AngleAxis(angles[index], transform.forward) * transform.up * ratioMultiplier;
            vector = vector.normalized * 10;

            index++;
            if (index >= angles.Count) index = 0;

            
            scr3.Create(gameObject111, transform.position, vector, gameObject.name);
        }
    }

    
   

    

   
    
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
