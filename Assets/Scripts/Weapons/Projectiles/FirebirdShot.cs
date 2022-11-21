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
    IEntity source;
    
    void OnEnable()
    {
        
        //Destroy(gameObject, timeOfLife);

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
        IDamagable eh = other.gameObject.GetComponentInParent<IDamagable>(false);
        if (eh != null)
        {

            if (eh.Gameobject == source.Gameobject) //Collision with player (after the shot for example)
                return;

            if (eh.Gameobject != alreadyHit && !eh.IsDead) //Checking if target is alive and wasnt already hit by this shot
            {
                eh.DealDamage(damage, source);
                
                alreadyHit = eh.Gameobject; //Setting hit gameobject as already hit for fixing double damaging one target by a single shot
                
            }
            

        }

        if (other.gameObject.layer == 7 || other.gameObject.layer == 9) //Checking if collided object is Environment or Ground
        {
            
            Destroy(gameObject);
        }

    }

    //Creating the shot prefab 
    public static void CreateShot(GameObject prefab, Vector3 pos, Vector3 velocityVector, IEntity source, float dmg, float tol)
    {
        GameObject go = Instantiate(prefab, pos, Camera.main.transform.rotation);
        go.GetComponent<Rigidbody>().velocity = velocityVector;
        go.GetComponent<FirebirdShot>().damage = dmg;
        go.GetComponent<FirebirdShot>().source = source;

        //Destroying shot after expiring its ToL
        Destroy(go, tol);

    }
}
