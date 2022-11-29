using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public GameObject explosion;
    IEntity source;
    float damage;
    float area;
    Rigidbody rb;
    Vector3 target;
    public float speed;
    //ParticleSystem exlosSize;
    
    
    void Start()
    {
        transform.LookAt(target, Vector3.up);

        rb = GetComponent<Rigidbody>();
        rb.velocity = (target - transform.position).normalized * speed;
    }

    

    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        EXPLOSON111();
    }
        

    public static void LaunchMissile(GameObject missilePrefab, Vector3 target, float dmg, float aoe, IEntity source)
    {
        MissileScript newMissile = Instantiate(missilePrefab, target + Vector3.up * 120, Quaternion.identity).GetComponent<MissileScript>();
        newMissile.damage = dmg;
        newMissile.area = aoe;
        newMissile.source = source;
        newMissile.target = target;     

    }
    void EXPLOSON111()
    {
        List<IDamagable> alreadyHit = new();

        List<Collider> hitList = new(Physics.OverlapSphere(transform.position, area, source.EnemiesMasks));

        foreach (Collider item in hitList)
        {            
            
            if (TryGetComponent(out IDamagable damagable))
            {
                if (!alreadyHit.Contains(damagable))
                {
                    damagable.DealAOE(new Damage(damage, source));
                    alreadyHit.Add(damagable);
                }
            }

        }
        GameObject explosFX = Instantiate(explosion, transform.position, transform.rotation);
        explosFX.transform.localScale *= area;
        var main = explosFX.GetComponentInChildren<ParticleSystem>().main;
        main.startSize = 2.2f * area;
        Destroy(explosFX, 3);
        Destroy(gameObject);
    }
}
