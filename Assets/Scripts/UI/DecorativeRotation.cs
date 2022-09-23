using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorativeRotation : MonoBehaviour
{
    public float rotationSpeed;
    void Start()
    {
        Debug.Log(Vector3.up);
        Debug.Log(transform.up);
    }

   
    void Update()
    {
        //transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        //transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + rotationSpeed * Time.deltaTime, transform.localEulerAngles.z);
    }
}
