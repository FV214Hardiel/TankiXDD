using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    EntityHandler eh;

    Transform target;
    float rotSpeed;

    Vector3 relPosH;
    Vector3 relPosV;
    Quaternion InnerRotQH;
    
    float TargetPos;
    public Vector2 minmax;

    GameObject gun;


    private void Start()
    {
        eh = GetComponentInParent<EntityHandler>();
        rotSpeed = eh.turretMod.turretRotationSpeed * Time.fixedDeltaTime; 

        target = GameObject.Find("Poinet").transform; //Find invisible helping GO
        gun = transform.Find("barrel").gameObject; //GO for gun depression
    }

    void FixedUpdate()
    {           
        relPosH = Vector3.ProjectOnPlane((target.position - transform.position), transform.up); //Projecting on Y plane a vector between camera pointer and turret center
        InnerRotQH = Quaternion.LookRotation(relPosH, transform.up);        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, InnerRotQH, rotSpeed); //Rotating turret on Y axis 

        relPosV = Vector3.ProjectOnPlane((target.position - gun.transform.position), gun.transform.right);  //Projecting on X plane a vector between pointer and gun center     
        TargetPos = Vector3.SignedAngle(relPosV, transform.forward, -gun.transform.right);
        TargetPos = Mathf.Clamp(TargetPos, minmax.x, minmax.y); //Clamping targeting angles
        gun.transform.localEulerAngles = new Vector3(TargetPos, 0, 0);
    }
    
}

