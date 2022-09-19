using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmoky : PlayerShooting
{ 
    public float delayBetweenShots;
    float remainingDelay;    

    public Transform muzzle;

    public float weapRange;

    
    public float angle;

    List<ushort> disperseAngles;
    List<float> disperseLengths;
    int index;

    AudioSource shotSound;
    AudioSource chargeSound;

    public GameObject prefabOfShot;
    public ParticleSystem shotEffect;
    public ParticleSystem hitEffect;

    PlayerInputActions inputActions;
    float inputValue;

    void Start()
    {
        source = GetComponentInParent<EntityHandler>().gameObject;
        muzzle = transform.Find("muzzle");

        inputActions = new();
        if (!GameHandler.GameIsPaused) inputActions.PlayerTankControl.Enable();

        shotSound = GetComponent<AudioSource>();
        //chargeSound = transform.Find("ChargesSound").GetComponent<AudioSource>();

        remainingDelay = 0;

        



    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.GameIsPaused) return; //Checking pause
               
        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }     

        inputValue = inputActions.PlayerTankControl.Fire.ReadValue<float>();

        if (inputValue > 0) //Shot
        {
            //shotVector = DisperseVector(muzzle.forward, angle);
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

    

//{ //public Light shotLight;

//    public float weapRange;

//    public float reloadTime;
//    float remCD;

//    Transform muzzle;

//    float wait;
//    public GameObject expPref;
//    public GameObject[] ShotEff;
//    AudioSource shotAudio;

//    AudioSource hitAudio;

//    void Start()
//    {
//        hitAudio = GameObject.Find("HitSFX").GetComponent<AudioSource>();
//        shotAudio = GetComponent<AudioSource>();

//        remCD = 0;

//        muzzle = transform.Find("muzzle");
//        wait = 0.1f;


//    }

//    // Update is called once per frame
//    void Update()
//    {   if (remCD > 0)
//        {
//            remCD -= Time.deltaTime;
//        }

//        if (!GameHandler.GameIsPaused && Input.GetButtonDown("Fire1") && remCD <= 0)
//        {
//            remCD = reloadTime;
//            StartCoroutine(ShotEffect());
//        }
//    }

//    IEnumerator ShotEffect()
//    {
//        RaycastHit hit;
//        foreach (GameObject sht in ShotEff)
//        {
//            sht.SetActive(true);
//        }
//        shotAudio.Play();
//        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, weapRange))
//        {
//            // Debug.Log(hit.collider);            

//            Health health = hit.collider.GetComponentInParent<Health>();
//            if (health != null)
//            {
//                health.TakingDMG(damage, source);


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
