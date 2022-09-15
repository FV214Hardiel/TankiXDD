using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Transform Target;
    public float RotSpeed;
    Vector3 relPosH;
    Vector3 relPosV;
    Quaternion InnerRotQH;
    
    float TargetPos;
    public Vector2 minmax;

    public GameObject gun;


    private void Start()
    {
        Target = GameObject.Find("Poinet").transform;
    }

    void FixedUpdate()
    {   


        relPosH = Vector3.ProjectOnPlane((Target.position - transform.position), transform.up);
        InnerRotQH = Quaternion.LookRotation(relPosH, transform.up);        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, InnerRotQH, RotSpeed);

        relPosV = Vector3.ProjectOnPlane((Target.position - gun.transform.position), gun.transform.right);       
        TargetPos = Vector3.SignedAngle(relPosV, transform.forward, -gun.transform.right);
        TargetPos = Mathf.Clamp(TargetPos, minmax.x, minmax.y);
        gun.transform.localEulerAngles = new Vector3(TargetPos, 0, 0);

        

    }


    /* Старый раздельный поворот (отдельно башня, отдельно пушка)
    void FixedUpdate()
    {
        relPos = Vector3.ProjectOnPlane((Target.position - transform.position), transform.up);
        InnerRotQ = Quaternion.LookRotation(relPos, transform.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, InnerRotQ, RotSpeed);
    }
    */
}

/* void Start()
 {
       Поворот с помощью клавиатуры

      hinge = gameObject.GetComponent<HingeJoint>();
      hingeSpring = hinge.spring;
      Debug.Log(hingeSpring.spring);
      hingeSpring.spring = 300;
      hingeSpring.targetPosition = 0;
      hinge.spring = hingeSpring;

      Поворот камерой, первая версия

     hinge = gameObject.GetComponent<HingeJoint>();
     hingeSpring = hinge.spring;
     hingeSpring.spring = 300;
     hingeSpring.damper = 120;
     hingeSpring.targetPosition = PlayerCamera.transform.rotation.y;
     hinge.spring = hingeSpring;



 }


void FixedUpdate()
{

transform.rotation = Quaternion.RotateTowards(transform.rotation, Pointer.rotation, 0.5f);

/* Поворот клавиатурой

Rot = Input.GetAxis("TurretRot");
hingeSpring.targetPosition += Rot;
if (hingeSpring.targetPosition > 180)
{
    hingeSpring.targetPosition = hingeSpring.targetPosition - 360;
}
if (hingeSpring.targetPosition <= -180)
{
    hingeSpring.targetPosition = hingeSpring.targetPosition + 360;
}
hinge.spring = hingeSpring;


 Поворот мышкой, первая версия

hingeSpring.targetPosition = PlayerCamera.transform.localEulerAngles.y - HullBody.transform.localEulerAngles.y;
if (hingeSpring.targetPosition > 180)
{
    hingeSpring.targetPosition = hingeSpring.targetPosition - 360;
}
if (hingeSpring.targetPosition <= -180)
{
    hingeSpring.targetPosition = hingeSpring.targetPosition + 360;
}
hinge.spring = hingeSpring;





}

*/