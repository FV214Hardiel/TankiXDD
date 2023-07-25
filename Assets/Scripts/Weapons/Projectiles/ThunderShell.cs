using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderShell : MonoBehaviour
{
    public float timeOfLife;
    public float timer;

    public IEntity source;

    public float damage;

    public float expRadius;
    public float expDamage;

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

    private void Start()
    {        
        rb = GetComponent<Rigidbody>();
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

        IDamagable damagable = other.GetComponentInParent<IDamagable>();
        if (damagable != null)
        {
            if (!damagable.IsDead)
            {

                damagable.DealDamage(new Damage(damage, source));
            }
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
            print(item);
            if (item.TryGetComponent(out IDamagable damagable))
            {
                damagable.DealEMP(new Damage(expDamage, source));               
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

                IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();
                if (damagable != null)
                {
                    if (!damagable.IsDead)
                    {
                        damagable.DealDamage(new Damage(pelletDamage, source));
                    }
                }

            }
            else
            {
                WeaponTrail.Create(trail, transform.position, transform.position + shotVector * pelletsDistance); //Shot VFX if no hit
            }

        }
        Destroy(gameObject);
    }


    
    //public static void CreateShot(GameObject prefab, Vector3 pos, Vector3 velocityVector, IEntity source, float dmg, float tol)
    //{
    //    GameObject go = Instantiate(prefab, pos, Camera.main.transform.rotation);
    //    go.GetComponent<Rigidbody>().velocity = velocityVector;
    //    ThunderShell sht = go.GetComponent<ThunderShell>();
    //    sht.expDamage = dmg;
    //    sht.source = source;
    //    sht.timer = tol;
        
    //}

    //public static void CreatePellets(GameObject trail, Vector3 pos, Vector3 forwardVector, IEntity source,
    //    int pelletsCount, float pelletDamage, float pelletsAngle, float pelletsDistance, Vector3 upVector)
    //{
        

    //    Vector3 shotVector = Vector3.zero;

    //    RaycastHit hit;
    //    for (int i = 0; i < pelletsCount; i++)
    //    {
    //        shotVector = DisperseVector(forwardVector, pelletsAngle, upVector);

    //        if (Physics.Raycast(pos, shotVector, out hit, pelletsDistance))
    //        {
    //            //WeaponTrail.Create(trail, forwardVector, hit.point);

    //            WeaponTrail.Create(trail, pos, hit.point);


    //            IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();
    //            if (damagable != null)
    //            {
    //                if (!damagable.IsDead)
    //                {

    //                    damagable.DealDamage(new Damage(pelletDamage, source));
    //                }
    //            }

    //        }
    //        else
    //        {
    //            WeaponTrail.Create(trail, pos, pos + shotVector * pelletsDistance); //Shot VFX if no hit
    //        }

    //    }
    //    //Destroy(go.gameObject);
    //}

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

    //static Vector3 DisperseVector(Vector3 originalVector, float angle, Vector3 upVector)
    //{
    //    Vector3 vector = originalVector.normalized; //Original vector must be normalized

    //    //Taking random values from pregenerated lists       

    //    angle *= Mathf.Deg2Rad; //Angle from degrees to rads

    //    float ratioMultiplier = Mathf.Tan(angle); //Tangens of angle for ratio between Dispersion Leg and Base Leg       

    //    //Adding UP vector multiplied by ratio and random value and rotated on random angle
    //    vector += Quaternion.AngleAxis(Random.Range(1, 357), originalVector) * (Random.Range(0f, 1f) * ratioMultiplier * upVector);

    //    return vector.normalized;
    //}
}
