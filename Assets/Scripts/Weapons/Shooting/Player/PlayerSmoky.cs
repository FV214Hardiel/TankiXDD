using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmoky : PlayerShooting
{           
    public ParticleSystem shotEffect;
    public ParticleSystem hitEffect;

    [SerializeField]
    Health health;

    void Start()
    {
        source = GetComponentInParent<EntityHandler>();
        muzzle = transform.Find("muzzle");

        inputActions = new();
        if (!GameHandler.instance.GameIsPaused) inputActions.PlayerTankControl.Enable();

        shotDelegate = Shot;

        shotSound = GetComponent<AudioSource>();
        //chargeSound = transform.Find("ChargesSound").GetComponent<AudioSource>();

        health = source.HealthScript;

        remainingDelay = 0;

    }

    public override void EnableOverload()
    {
        base.EnableOverload();
        //if (health!= null)
        //{
        //    health = GetComponentInParent<Health>(true);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.instance.GameIsPaused) return; //Checking pause
               
        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }     

        inputValue = inputActions.PlayerTankControl.Fire.ReadValue<float>();

        if (inputValue > 0) //Shot
        {
            //shotVector = DisperseVector(muzzle.forward, angle);
            shotDelegate();
        }
    }

    

    protected override void Shot()
    {
        base.Shot();

        shotSound.Play();
        shotEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, weapRange))
        {
            
            IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {
                //print("Damageble");
                if (!damagable.IsDead)
                {
                    damagable.DealDamage(new Damage(shotDamage, source));
                   
                }
            }
            hitEffect.transform.position = hit.point;
            hitEffect.Play();
            
        }
        remainingDelay = delayBetweenShots;
    }

    protected override void OverloadShot()
    {
        base.OverloadShot();
        Shot();

        health.Heal(damage * 0.5f, source);
        
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

//        if (!(GameHandler.instance.GameIsPaused && Input.GetButtonDown("Fire1") && remCD <= 0)
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
