using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphhover : MonoBehaviour
{
    public Rigidbody rb;
    public float dampering;
    public float strength;
    public float length;
    float lastHitDist;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, length))
        {
            float forceAmount = HooksLawDampen(hit.distance);
            rb.AddForceAtPosition(transform.up * forceAmount, transform.position);
        }
        else
        {
            lastHitDist = length * 1.1f;
        }
    }
    private float HooksLawDampen(float hitDistance)
    {
        float forceAmount = strength * (length - hitDistance) + (dampering * (lastHitDist - hitDistance));
        forceAmount = Mathf.Max(0f, forceAmount);
        lastHitDist = hitDistance;

        return forceAmount;
    }
}
