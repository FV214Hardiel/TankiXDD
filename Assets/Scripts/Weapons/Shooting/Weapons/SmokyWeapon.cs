using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SmokyWeapon : Weapon
{           
    public ParticleSystem shotEffect;
    public ParticleSystem hitEffect;

    //[SerializeField]
    Health health;

    new void Start()
    {
        base.Start();

        hitDelegate = BasicHit;        

        health = source.HealthScript;

    }

    public override void EnableOverload()
    {
        base.EnableOverload();        
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
                    // damagable.DealDamage(new Damage(shotDamage, source));
                    hitDelegate.Invoke(damagable, new Damage(shotDamage, source));

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
                    //damagable.DealDamage(new Damage(shotDamage, source));
                    hitDelegate.Invoke(damagable, new Damage(shotDamage, source));
                    health.Heal(damage * 0.5f, source);

                }
            }
            hitEffect.transform.position = hit.point;
            hitEffect.Play();

        }
        remainingDelay = delayBetweenShots;        
    }

    protected override void BasicHit(IDamagable hit, Damage dmg)
    {
        hit.DealDamage(dmg);
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
