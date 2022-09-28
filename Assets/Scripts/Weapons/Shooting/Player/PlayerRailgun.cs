using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRailgun : PlayerShooting
{
    

    Transform muzzle; 

    public float weapRange;    

    public float delayBetweenShots;
    float remainingDelay;

    PlayerInputActions inputActions;
    float inputValue;


    ParticleSystem chargeLight;
    public float chargeDuration;
    AudioSource chargeSound;

    public GameObject prefabOfShot;

    AudioSource shotSound;



    void Start()
    {
        source = GetComponentInParent<EntityHandler>().gameObject;
        muzzle = transform.Find("muzzle");

        inputActions = new();
        if (!GameHandler.GameIsPaused) inputActions.PlayerTankControl.Enable();

        shotSound = GetComponent<AudioSource>();
        chargeSound = transform.Find("ChargeSound").GetComponent<AudioSource>();

        chargeLight = transform.Find("muzzleFlash").GetComponent<ParticleSystem>();

        remainingDelay = 0;
    }

    
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
            StartCoroutine(Shot());
        }
    }

    IEnumerator Shot()
    {
        remainingDelay = delayBetweenShots;
        chargeSound.Play();
        chargeLight.Play();

        yield return new WaitForSeconds(chargeDuration);
        chargeSound.Stop();
        chargeLight.Stop();

        shotSound.Play();

        
        if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, weapRange))
        {
            Debug.Log(hit.collider);
            EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>(false);
            if (eh != null)
            {
                
                if (!eh.isDead) //Checking if target is alive and wasnt already hit by this shot
                {
                    eh.DealDamage(damage, source);
                }
            }

            WeaponTrail.Create(prefabOfShot, muzzle.position, hit.point); //Shot VFX if hit
        }
        else
        {
            WeaponTrail.Create(prefabOfShot, muzzle.position, muzzle.position + muzzle.forward * weapRange); //Shot VFX if no hit
        }

        remainingDelay = delayBetweenShots;
    }
    
}


//LineRenderer laserLine;

//Light shotLight;
//public Light hitLight;
//Color c;

//Transform muzzle;

//public float shotDurF;
//WaitForSeconds shotDuration;

//public float chargDurF;
//WaitForSeconds chargeDuration;

//public float weapRange;


////public float reloadTime;
////float nextShot;

//public float delayBetweenShots;
//float remainingDelay;

//PlayerInputActions inputActions;

////WaitForSeconds reloadTime;

//void Start()
//{
//    laserLine = GetComponent<LineRenderer>();

//    inputActions = new();

//    shotLight = muzzle.GetComponent<Light>();

//    shotDuration = new WaitForSeconds((shotDurF - 0.04f) / 10f);
//    chargeDuration = new WaitForSeconds(chargDurF / 10f);
//    c = laserLine.material.color;
//}


//void Update()
//{

//    if (GameHandler.GameIsPaused) return; //Checking pause

//    if (remainingDelay > 0) remainingDelay -= Time.deltaTime; //Decreasing delay timer 

//    if (remainingDelay <= 0 && inputActions.PlayerTankControl.Fire.IsPressed())
//    {
//        remainingDelay = delayBetweenShots;
//        StartCoroutine(ShotEffect());

//    }
//}


//private IEnumerator ShotEffect()
//{
//    shotLight.enabled = true;
//    shotLight.intensity = 0f;
//    for (float lI = 0; lI < 9; lI += 1)
//    {
//        shotLight.intensity += 0.3f;
//        yield return chargeDuration;
//    }

//    laserLine.SetPosition(0, muzzle.transform.position);
//    RaycastHit hit;

//    if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, weapRange))
//    {
//        //Debug.Log(hit.point);
//        hitLight.transform.position = hit.point;
//        laserLine.SetPosition(1, hit.point);

//        //Health health = hit.collider.GetComponentInParent<Health>();            
//        //if (health != null)
//        //{
//        //    health.TakingDMG(damage, source);
//        //}

//        EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>();
//        if (eh != null)
//        {
//            eh.DealDamage(damage, source);
//        }
//    }
//    else
//    {
//        //Debug.Log(hit.point);
//        laserLine.SetPosition(1, muzzle.position + weapRange * muzzle.forward);

//    }
//    laserLine.enabled = true;
//    c.a = 1;
//    laserLine.material.color = c;
//    hitLight.enabled = true;
//    yield return new WaitForSeconds(0.04f);
//    hitLight.enabled = false;
//    for (float lI = 0; lI < 9; lI += 1)
//    {
//        //hitLight.transform.position = hit.point;
//        shotLight.intensity -= 2f;
//        c.a -= 0.1f;
//        laserLine.material.color = c;
//        yield return shotDuration;

//    }
//    hitLight.enabled = false;
//    laserLine.enabled = false;




//}
