using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIRailgun : AIShooting
{

    Transform muzzle;

    public float weapRange;

    public float delayBetweenShots;
    float remainingDelay;    


    ParticleSystem chargeLight;
    public float chargeDuration;
    AudioSource chargeSound;

    public GameObject prefabOfShot;

    AudioSource shotSound;

    AIMove ai;
    bool isTargetLocked;

    Ray lineOfFire;
    RaycastHit hit;
    LayerMask enemyMask;


    void Start()
    {
        source = GetComponentInParent<EntityHandler>();
        muzzle = transform.Find("muzzle");

        ai = gameObject.GetComponentInParent<AIMove>();
        enemyMask = ai.enemyLayers;        

        shotSound = GetComponent<AudioSource>();
        chargeSound = transform.Find("ChargeSound").GetComponent<AudioSource>();

        chargeLight = transform.Find("muzzleFlash").GetComponent<ParticleSystem>();

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
            if (ai.AIState == AIMove.AIEnum.Attack && !isStunned)
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


        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, weapRange))
        {
           
            EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>(false);
            if (eh != null)
            {

                if (!eh.isDead) //Checking if target is alive
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
