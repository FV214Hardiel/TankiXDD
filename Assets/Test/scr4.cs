using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr4 : MonoBehaviour
{
    Collider[] colliders;
    public GameObject item;

    //public LayerMask lyaerMask;
    int lm;
    void Start()
    {

        if (Physics.Linecast(transform.position, item.transform.position, out RaycastHit hit))
        {
            if (hit.collider.gameObject == item)
            {
                Debug.Log("Path is clear");
            }
            else
            {
                Debug.Log("Path is blocked by " + hit.collider.name);
            }
            
        }
        //StartCoroutine(CustomUpdate());

    }

    

    public IEnumerator CustomUpdate()
    {
        
        while (true)
        {
            //Debug.Log(Time.time);
            colliders = Physics.OverlapSphere(transform.position, 20f, ~3);
            foreach (Collider item in colliders)
            {
                Debug.Log(item.name);
            }

            yield return new WaitForSeconds(2);
        }
    }
}


