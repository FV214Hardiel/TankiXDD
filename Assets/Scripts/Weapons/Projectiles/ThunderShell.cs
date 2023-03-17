using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShell : MonoBehaviour
{
    float timeOfLife;
    float timer;

    public IEntity source;
    public float expForce;
    public float expRadius;
    public float damage;
    //AudioSource expSound;

    public int pelletsCount;
    public float pelletsDistance;
    public float pelletsAngle;
    public float pelletDamage;

    bool alreadyHit;
    
    Vector3 shotVector;

    [SerializeField]
    GameObject explosion;
    public GameObject trail;

    Rigidbody rb;

    private void OnEnable()
    {
        //expSound = GetComponent<AudioSource>();
        timer = timeOfLife;
        rb = GetComponent<Rigidbody>();
        //explosion = transform.Find("Explosion");
        alreadyHit = false;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Explosion();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) return;
        if (other.GetComponentInParent<TankEntity>())
        {
            other.GetComponentInParent<TankEntity>().DealDamage(new Damage(30, source));
            alreadyHit = true;
        }
        Destroy(gameObject);
    }

    private void Explosion()
    {
        //Creating EMP VFX
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        exp.SetActive(true);
        Destroy(exp, 3);
        exp.GetComponent<AudioSource>().Play();

        //EMP
        List<IEntity> alreadyHit = new();
        List<Collider> hitList = new (Physics.OverlapSphere(transform.position, expRadius, source.EnemiesMasks));
        foreach (Collider item in hitList)
        {            

            if (item.TryGetComponent(out IDamagable damagable))
            {


                damagable.DealEMP(new Damage(damage, source));
               
                alreadyHit.Add(damagable.Entity);
            }
            
        }


        //Pellets
        RaycastHit hit;
        for (int i = 0; i < pelletsCount; i++)
        {
            shotVector = DisperseVector(transform.forward, pelletsAngle);

            if (Physics.Raycast(transform.position, shotVector, out hit, pelletsDistance))
            {

                
                WeaponTrail.Create(trail, transform.position, hit.point);
                //EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>(false);
                //if (eh != null)
                //{
                //    if (!eh.IsDead) //Checking if target is alive and wasnt already hit by this shot
                //    {
                //        eh.DealDamage(pelletDamage, source);

                //    }
                //}

                if (hit.collider.TryGetComponent(out IDamagable damagable))
                {
                    damagable.DealDamage(new Damage(damage, source));                   
                }


            }
            {
                WeaponTrail.Create(trail, transform.position, transform.position + shotVector * pelletsDistance); //Shot VFX if no hit
            }

        }
        Destroy(gameObject);

    }


    
    public static void CreateShot(GameObject prefab, Vector3 pos, Vector3 velocityVector, IEntity source, float dmg, float tol)
    {
        GameObject go = Instantiate(prefab, pos, Camera.main.transform.rotation);
        go.GetComponent<Rigidbody>().velocity = velocityVector;
        ThunderShell sht = go.GetComponent<ThunderShell>();
        sht.damage = dmg;
        sht.source = source;
        sht.timer = tol;
        
    }

    Vector3 DisperseVector(Vector3 originalVector, float angle)
    {
        Vector3 vector = originalVector.normalized; //Original vector must be normalized

        //Taking random values from pregenerated lists       

        angle *= Mathf.Deg2Rad; //Angle from degrees to rads

        float ratioMultiplier = Mathf.Tan(angle); //Tangens of angle for ratio between Dispersion Leg and Base Leg       

        //Adding UP vector multiplied by ratio and random value and rotated on random angle
        vector += Quaternion.AngleAxis(Random.Range(1, 357), originalVector) * (Random.Range(0f, 1f) * ratioMultiplier * transform.up);

        return vector.normalized;
    }
}
