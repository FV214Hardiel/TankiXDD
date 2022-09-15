using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnemyRotation : MonoBehaviour
{
    Transform mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main.transform;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = mainCamera.rotation;
    }
}
