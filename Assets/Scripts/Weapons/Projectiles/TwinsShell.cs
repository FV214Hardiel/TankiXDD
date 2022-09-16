using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinsShell : MonoBehaviour
{
    [SerializeField] float timeOfLife;

    public GameObject source;
    public float expForce;
    public float expRadius;
    public float damage;

    public Effect debuff;

    GameObject alreadyHit;
    private void OnEnable()
    {
        //Destroying shot after expiring its ToL
        Destroy(gameObject, timeOfLife);
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

                //Adding debuff and increasing damage if debuff exists on target
                Effect twinsEffect = eh.effh.GetEffect(debuff.effectID);
                if (twinsEffect == null)
                {
                    
                    eh.effh.AddEffect(debuff);
                }
                else
                {                   

                    damage += 3 * twinsEffect.effectStacks;
                    eh.effh.AddEffect(debuff);
                }
                
                eh.DealDamage(damage, source);
                alreadyHit = eh.gameObject; //Setting hit gameobject as already hit for fixing double damaging one target by a single shot
                Destroy(gameObject);

            }

            
        }

        
        

    }

    //Creating the shot prefab 
    public static void CreateShot(GameObject prefab, Vector3 pos, Vector3 velocityVector, GameObject source, float dmg, Effect debuff, float tol)
    {
        GameObject go = Instantiate(prefab, pos, Camera.main.transform.rotation);
        go.GetComponent<Rigidbody>().velocity = velocityVector;
        TwinsShell sht = go.GetComponent<TwinsShell>();
        sht.damage = dmg;
        sht.source = source;
        sht.debuff = debuff;

        //Destroying shot after expiring its ToL
        Destroy(go, tol);
    }

}
