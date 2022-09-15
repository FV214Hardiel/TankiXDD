using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTarget : MonoBehaviour
{

   

    Camera playerCamera;
    Transform barrel;
    RaycastHit hit;
    public Transform groundTarget;



    private void Start()
    {
        barrel = transform;
        playerCamera = Camera.main;
        groundTarget = GameObject.Find("GroundTargetUI").transform;
    }
    void FixedUpdate()
    {
        if (Physics.Raycast(barrel.position, barrel.transform.forward, out hit))
        {
            groundTarget.position = playerCamera.WorldToScreenPoint(hit.point);
            //playerCamera.WorldToScreenPoint(hit.point);
            //Debug.Log(playerCamera.WorldToScreenPoint(hit.point));
        }
        else
        {
            groundTarget.position = playerCamera.WorldToScreenPoint(barrel.position + barrel.forward * 100f);
        }
        
    }
    
}
