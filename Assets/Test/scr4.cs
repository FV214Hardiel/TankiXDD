using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr4 : MonoBehaviour
{
    Collider[] colliders;
    public scr5 item;

    //public LayerMask lyaerMask;
    int lm;

    // RECEIVER
    void Start()
    {
        scr5.isEvent += isReceived;
        

    }

    void isReceived(GameObject other)
    {
        print(gameObject.name + " has received signal from " + other.name);
    }
    

    //public IEnumerator CustomUpdate()
    //{
        
    //    while (true)
    //    {
    //        //Debug.Log(Time.time);
    //        colliders = Physics.OverlapSphere(transform.position, 20f, ~3);
    //        foreach (Collider item in colliders)
    //        {
    //            Debug.Log(item.name);
    //        }

    //        yield return new WaitForSeconds(2);
    //    }
    //}
}


