using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemyRotation : MonoBehaviour
{
    Transform mainCamera;
    
    void Start()
    {
        mainCamera = Camera.main.transform;

    }

   
    void Update()
    {
        gameObject.transform.rotation = mainCamera.rotation;
    }
}
