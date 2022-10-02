using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr5 : MonoBehaviour
{
    // FALLING CUBE EMITTER
    public delegate void collided(GameObject x);
    public static event collided isEvent;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        isEvent?.Invoke(gameObject);
    }
}
