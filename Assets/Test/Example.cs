using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    private float speed = -2f;

    //Moves this GameObject 2 units a second in the forward direction
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Gun"))
        {
           Destroy(gameObject);
        }
           
    }
}