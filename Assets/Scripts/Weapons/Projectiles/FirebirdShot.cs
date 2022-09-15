using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebirdShot : MonoBehaviour
{
    [SerializeField] float timeOfLife;

    [SerializeField] float minScale;
    [SerializeField] float maxScale;
    float scaleCurrent;
    float scaleIncrement;

    GameObject alreadyHit;

    float damage;
    GameObject source;
    
    void Start()
    {
        //Destroying shot after expiring its ToL
        Destroy(gameObject, timeOfLife);

        //Increasing shot size 
        scaleCurrent = minScale; 
        transform.localScale = Vector3.one * scaleCurrent;
        scaleIncrement = (maxScale - minScale) / timeOfLife * Time.fixedDeltaTime; //Even increment from min/max values and ToL

    }

    
    void FixedUpdate()
    {
        //Increasing shot size
        scaleCurrent += scaleIncrement;
        transform.localScale = Vector3.one * scaleCurrent;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        //Getting GodClass
        EntityHandler eh = other.gameObject.GetComponentInParent<EntityHandler>(false);
        if (eh != null)
        {

            if (eh.gameObject == source) //Collision with player (after the shot for example)
                return;

            if (eh.gameObject != alreadyHit && !eh.isDead) //Checking if target is alive and wasnt already hit by this shot
            {
                eh.DealDamage(damage, source);
                
                alreadyHit = eh.gameObject; //Setting hit gameobject as already hit for fixing double damaging one target by a single shot
                
            }
            

        }

        if (other.gameObject.layer == 7 || other.gameObject.layer == 9) //Checking if collided object is Environment or Ground
        {
            
            Destroy(gameObject);
        }

    }

    //Creating the shot prefab 
    public static void CreateShot(GameObject prefab, Vector3 pos, Vector3 velocityVector, GameObject source, float dmg)
    {
        GameObject go = Instantiate(prefab, pos, Camera.main.transform.rotation);
        go.GetComponent<Rigidbody>().velocity = velocityVector;
        go.GetComponent<FirebirdShot>().damage = dmg;
        go.GetComponent<FirebirdShot>().source = source;
        Destroy(go, 4);

    }
}
