using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public GameObject Explosion;
    GameObject Source;
    float Damage;
    float Area;
    Rigidbody rb;
    Vector3 Target;
    public float speed;
    //ParticleSystem exlosSize;
    
    
    void Start()
    {
        transform.LookAt(Target, Vector3.up);

        rb = GetComponent<Rigidbody>();
        rb.velocity = (Target - transform.position).normalized * speed;
    }

    

    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        EXPLOSON111();
    }
        

    public static void LaunchMissile(GameObject missilePrefab, Vector3 target, float dmg, float aoe, GameObject source)
    {
        MissileScript newMissile = Instantiate(missilePrefab, target + Vector3.up * 120, Quaternion.identity).GetComponent<MissileScript>();
        newMissile.Damage = dmg;
        newMissile.Area = aoe;
        newMissile.Source = source;
        newMissile.Target = target;

        

    }
    void EXPLOSON111()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Area);
        foreach (Collider nearby in colliders)
        {            
            if (nearby.GetComponent<EntityHandler>())
            {
                nearby.GetComponent<EntityHandler>().DealDamage(Damage, Source);
            }
        }
        GameObject explosFX = Instantiate(Explosion, transform.position, transform.rotation);
        explosFX.transform.localScale *= Area;
        var main = explosFX.GetComponentInChildren<ParticleSystem>().main;
        main.startSize = 2.2f * Area;
        Destroy(explosFX, 3);
        Destroy(gameObject);
    }
}
