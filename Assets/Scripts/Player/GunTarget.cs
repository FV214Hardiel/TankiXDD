using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTarget : MonoBehaviour
{
    Camera playerCamera;

    RaycastHit hit;

    Transform groundTarget;


    private void Start()
    {
        playerCamera = Camera.main;
        groundTarget = GameObject.Find("GroundTargetUI").transform;
    }
    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.transform.forward, out hit))
        {
            groundTarget.position = playerCamera.WorldToScreenPoint(hit.point);            
        }
        else
        {
            groundTarget.position = playerCamera.WorldToScreenPoint(transform.position + transform.forward * 100f);
        }
        
    }
    
}
