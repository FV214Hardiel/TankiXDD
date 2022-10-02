using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShell : MonoBehaviour
{
    [SerializeField] float timeOfLife;
    float timer;

    public EntityHandler source;
    public float expForce;
    public float expRadius;
    public float damage;
    //AudioSource expSound;

    public int pelletsCount;
    public float pelletsDistance;
    public float pelletsAngle;
    public float pelletDamage;

    
    
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
        if (other.GetComponent<EntityHandler>())
        {
            other.GetComponent<EntityHandler>().DealDamage(30, source);
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
        List<EntityHandler> alreadyHit = new();
        List<Collider> hitList = new (Physics.OverlapSphere(transform.position, expRadius, source.enemiesMask));
        foreach (Collider item in hitList)
        {
            EntityHandler eh = item.GetComponent<EntityHandler>();
            if (eh)
            {
                if (!alreadyHit.Contains(eh)) //if DOES NOT contain then DAMAGE
                {
                    eh.DealEMP(damage, source);
                    //eh.DealDamage(damage, source);
                    alreadyHit.Add(eh);
                }
                
            }
            
        }


        //Pellets
        RaycastHit hit;
        for (int i = 0; i < pelletsCount; i++)
        {
            shotVector = DisperseVector(transform.forward, pelletsAngle);

            if (Physics.Raycast(transform.position, shotVector, out hit, pelletsDistance))
            {

                print(hit.collider);
                WeaponTrail.Create(trail, transform.position, hit.point);
                EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>(false);
                if (eh != null)
                {
                    if (!eh.isDead) //Checking if target is alive and wasnt already hit by this shot
                    {
                        eh.DealDamage(pelletDamage, source);

                    }
                }


            }
            {
                WeaponTrail.Create(trail, transform.position, transform.position + shotVector * pelletsDistance); //Shot VFX if no hit
            }

        }
        Destroy(gameObject);

    }


    // Update is called once per frame
    public static void CreateShot(GameObject prefab, Vector3 pos, Vector3 velocityVector, EntityHandler source, float dmg, float tol)
    {
        GameObject go = Instantiate(prefab, pos, Camera.main.transform.rotation);
        go.GetComponent<Rigidbody>().velocity = velocityVector;
        ThunderShell sht = go.GetComponent<ThunderShell>();
        sht.damage = dmg;
        sht.source = source;

        //Destroying shot after expiring its ToL
        Destroy(go, tol);
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
