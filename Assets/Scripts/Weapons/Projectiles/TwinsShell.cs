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

    IEntity alreadyHit;

    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}
    private void OnTriggerEnter(Collider collision)
    {

        //Getting IDamagable

        //print(collision.name);
        
        IDamagable damagable = collision.GetComponentInParent<IDamagable>();
        if (damagable != null)
        {
            
            if (damagable.Entity == source)
            {
                return;
            }

            if (damagable.Entity != alreadyHit && !damagable.IsDead)
            {
                if (damagable.IsStatusAffectable)
                {
                    Effect twinsEffect = damagable.Entity.EffH.GetEffect(debuff.effectID);
                    if (twinsEffect == null)
                    {
                        damagable.Entity.EffH.AddEffect(debuff);

                    }
                    else
                    {
                        damage += debuff.effectPower * twinsEffect.effectStacks;
                        damagable.Entity.EffH.AddEffect(debuff);

                    }
                }
                
                

                
                damagable.DealDamage(new Damage(damage, source));
                //print(damage);
                alreadyHit = damagable.Entity; //Setting hit gameobject as already hit for fixing double damaging one target by a single shot
                Destroy(gameObject);
            }
        }
        
        Destroy(gameObject);

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
