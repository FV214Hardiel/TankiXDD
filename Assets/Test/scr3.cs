using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr3 : MonoBehaviour
{
    // Start is called before the first frame update
    Collider thisColl;
    string team;
    public LayerMask lyaerMask;
    int lm;

    void Start()
    {


        

    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public static void Create(GameObject gameObject111, Vector3 pos, Vector3 velocity111, string teamus)
    {
        GameObject go = Instantiate(gameObject111, pos, Camera.main.transform.rotation);
        go.GetComponent<Rigidbody>().velocity = velocity111;
        go.GetComponent<scr3>().team = teamus;
        Destroy(go, 4);
        
    }
    

}
