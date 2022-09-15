using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr3 : MonoBehaviour
{
    // Start is called before the first frame update
    Collider thisColl;
    string team;
    
    void Start()
    {
        thisColl = GetComponent<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(team);
        if (other.bounds.Contains(transform.position))
        {
            Debug.Log("Ubit na ENTER");
            Destroy(gameObject);
        }

    }

    public static void Create(GameObject gameObject111, Vector3 pos, Vector3 velocity111, string teamus)
    {
        GameObject go = Instantiate(gameObject111, pos, Camera.main.transform.rotation);
        go.GetComponent<Rigidbody>().velocity = velocity111;
        go.GetComponent<scr3>().team = teamus;
        Destroy(go, 4);
        
    }
    //private void OnTriggerStay(Collider other)
    //{
    //    //Debug.Log("Prosto EXIT");
    //    if (other.bounds.Contains(transform.position))
    //    {
    //        Debug.Log("Ubit na EXIT");
    //        Destroy(gameObject);
    //    }

    //}

}
