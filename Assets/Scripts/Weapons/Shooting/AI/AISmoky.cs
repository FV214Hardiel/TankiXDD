using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISmoky : AIShooting
{
    public float delayBetweenShots;
    float remainingDelay;

    public Transform muzzle;

    public float weapRange;

    public float angle;

    List<ushort> disperseAngles;
    List<float> disperseLengths;
    int index;       

    AIMove ai;
    bool isTargetLocked;

    Ray lineOfFire;
    RaycastHit hit;
    LayerMask enemyMask;

    AudioSource shotSound;
    public GameObject prefabOfShot;
    public ParticleSystem shotEffect;
    public ParticleSystem hitEffect;

   

    void Start()
    {
        source = GetComponentInParent<EntityHandler>();
        muzzle = transform.Find("muzzle");

        ai = gameObject.GetComponentInParent<AIMove>();
        enemyMask = ai.enemyLayers;

        shotSound = GetComponent<AudioSource>();        

        remainingDelay = 0;

        source.TankStunned += OnStun;
        source.TankAwaken += OnUnStun;

        StartCoroutine(CustomUpdate(0.3f));

    }

    private void OnDestroy()
    {
        source.TankStunned -= OnStun;
        source.TankAwaken -= OnUnStun;

    }

    protected override void OnStun()
    {
        base.OnStun();

    }

    protected override void OnUnStun()
    {
        base.OnUnStun();
        StartCoroutine(CustomUpdate(1));

    }

    public IEnumerator CustomUpdate(float timeDelta)
    {
        while (true)
        {
            if (ai.AIState == AIMove.AIEnum.Attack)
            {
                lineOfFire = new Ray(muzzle.position, muzzle.forward);
                isTargetLocked = Physics.Raycast(lineOfFire, out hit, weapRange, enemyMask);
            }
            else
            {
                isTargetLocked = false;
            }

            yield return new WaitForSeconds(timeDelta);
        }
    }

    void Update()
    {
        if (GameHandler.GameIsPaused || isStunned) return; //Checking pause

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }
        
        if (isTargetLocked) //Shot
        {            
            Shot(muzzle.forward);
        }
    }

    Vector3 DisperseVector(Vector3 originalVector, float angle)
    {
        Vector3 vector = originalVector.normalized; //Original vector must be normalized

        //Taking random values from pregenerated lists
        ushort angleDis = disperseAngles[index];
        float lenghtDis = disperseLengths[index];

        index++;
        if (index >= disperseAngles.Count) index = 0; //Cycling indexes

        angle *= Mathf.Deg2Rad; //Angle from degrees to rads

        float ratioMultiplier = Mathf.Tan(angle); //Tangens of angle for ratio between Dispersion Leg and Base Leg       

        //Adding UP vector multiplied by ratio and random value and rotated on random angle
        vector += Quaternion.AngleAxis(angleDis, originalVector) * (lenghtDis * ratioMultiplier * muzzle.up);

        return vector.normalized;
    }

    void Shot(Vector3 shotVector)
    {
        shotSound.Play();
        shotEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, shotVector, out hit, weapRange))
        {
            EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>(false);
            if (eh != null)
            {
                if (!eh.isDead) //Checking if target is alive and wasnt already hit by this shot
                {
                    eh.DealDamage(damage, source);

                }
            }
            hitEffect.transform.position = hit.point;
            hitEffect.Play();

        }
        remainingDelay = delayBetweenShots;

    }
}


//{

//    //public Light shotLight;

//    public float weapRange;
//    public float Damage;

//    public float reloadTime;
//    float nextShot;

//    Transform Muzzle;
//    AIMove hull;

//    Ray shotLine;
//    Ray lineOfFire;
//    RaycastHit hit;

//    float wait;
//    public GameObject expPref;
//    public GameObject[] ShotEff;

//    void Start()
//    {
//        //team = "Red enemy";

//        nextShot = Time.time;
//        hull = gameObject.GetComponentInParent<AIMove>();
//        Muzzle = transform.Find("muzzle");
//        wait = 0.1f;


//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (hull.AIState == AIMove.AIEnum.Attack)
//        {
//            //Debug.Log("ataka");
//            lineOfFire = new Ray(Muzzle.position, Muzzle.forward);

//            Physics.Raycast(lineOfFire, out hit);
//            if ((hit.collider == Player.PlayerHullColl || hit.collider == Player.PlayerTurretColl) && Time.time > nextShot)
//            {
//                //Debug.Log("nachinaem vistrel");
//                nextShot = Time.time + reloadTime;
//                StartCoroutine(ShotEffect());
//            }
//        }
//    }

//    IEnumerator ShotEffect()
//    {
//        RaycastHit hit;
//        foreach (GameObject sht in ShotEff)
//        {
//            sht.SetActive(true);
//        }
//        if (Physics.Raycast(Muzzle.position, Muzzle.forward, out hit, weapRange))
//        {
//            // Debug.Log(hit.collider);            

//            //Health health = hit.collider.GetComponentInParent<Health>();
//            //if (health != null)
//            //{
//            //    health.TakingDMG(Damage, source);
//            //}
//            EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>();
//            if (eh != null)
//            {
//                eh.DealDamage(Damage, source);
//            }

//            Destroy(Instantiate(expPref, hit.point, Camera.main.transform.rotation), 1);

//        }
//        yield return new WaitForSeconds(wait);
//        foreach (GameObject sht in ShotEff)
//        {
//            sht.SetActive(false);
//        }
//    }
//}
