using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollideScript : MonoBehaviour
{
    public GameObject Explosion;
    GameObject Source;
    float Damage;
    float Area;
    Rigidbody rb;
    Vector3 Target;
    public float speed;
    ParticleSystem exlosSize;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = (Target - transform.position).normalized * speed;
    }

    

    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {

        EXPLOSON111();
        //Debug.Log(other);
    }

    public void MissileStats(float dmg, float aoe, Vector3 targetpos, GameObject source)
    {
        Damage = dmg;
        Area = aoe;
        Target = targetpos;
        Source = source;
    } 
    void EXPLOSON111()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, Area);
        foreach (Collider nearby in colliders)
        {
            //Debug.Log(nearby);

            //if (nearby.GetComponent<Health>())
            //{
            //    nearby.GetComponent<Health>().TakingDMG(Damage, Source);

            //    //Debug.Log(nearby.ToString() + " " + Damage.ToString());
            //}

            if (nearby.GetComponent<EntityHandler>())
            {
                nearby.GetComponent<EntityHandler>().DealDamage(Damage, Source);

                //Debug.Log(nearby.ToString() + " " + Damage.ToString());
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
