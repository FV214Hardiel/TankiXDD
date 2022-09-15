using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public GameObject shellPref;
    public float shellVeloc;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameHandler.GameIsPaused && Input.GetButtonDown("Fire1"))
        {
            //GameObject ShellFired = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            GameObject ShellFired = Instantiate(shellPref, transform.position + transform.up, transform.rotation);
            ShellFired.GetComponent<Rigidbody>().velocity = transform.up * shellVeloc;
            Debug.Log("SOZDAN");
            Destroy(ShellFired, 1);
            //ShellFired.transform.position = gameObject.transform.position;
        }
    }
}
