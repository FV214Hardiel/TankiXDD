using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinsShell : MonoBehaviour
{
    [SerializeField] float timeOfLife;

    public IEntity source;
    public float expForce;
    public float expRadius;
    public float damage;

    public Effect debuff;

    IDamagable alreadyHit;

    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}
    private void OnTriggerEnter(Collider collision)
    {

        //Getting IDamagable
        
        IDamagable damagable = collision.GetComponentInParent<IDamagable>();
        if (damagable != null)
        {
            
            if (damagable.Entity == source)
            {
                return;
            }

            if (damagable != alreadyHit && !damagable.IsDead)
            {
                
                Effect twinsEffect = damagable.Entity.EffH.GetEffect(debuff.effectID);
                if (twinsEffect == null)
                {
                    damagable.Entity.EffH.AddEffect(debuff);
                }
                else
                {
                    damage += 3 * twinsEffect.effectStacks;
                    damagable.Entity.EffH.AddEffect(debuff);
                }

                damagable.DealDamage(new Damage(damage, source));
                alreadyHit = damagable; //Setting hit gameobject as already hit for fixing double damaging one target by a single shot
                Destroy(gameObject);
            }
        }
        
        Destroy(gameObject);
        //IEntity eh = other.gameObject.GetComponentInParent<IEntity>(false);
        //if (eh != null)
        //{

        //    if (eh == source) //Collision with player (after the shot for example)
        //        return;

        //    if (eh.ThisGameobject != alreadyHit && !eh.IsDead) //Checking if target is alive and wasnt already hit by this shot
        //    {

        //        //Adding debuff and increasing damage if debuff exists on target
        //        Effect twinsEffect = eh.effh.GetEffect(debuff.effectID);
        //        if (twinsEffect == null)
        //        {
                    
        //            eh.effh.AddEffect(debuff);
        //        }
        //        else
        //        {                   

        //            damage += 3 * twinsEffect.effectStacks;
        //            eh.effh.AddEffect(debuff);
        //        }
                
        //        eh.DealDamage(damage, source);
        //        alreadyHit = eh.gameObject; //Setting hit gameobject as already hit for fixing double damaging one target by a single shot
        //        Destroy(gameObject);

        //    }

            
        //}

        
        

    }

    //Creating the shot prefab 
    public static void CreateShot(GameObject prefab, Vector3 pos, Vector3 velocityVector, IEntity source, float dmg, Effect debuff, float tol)
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
