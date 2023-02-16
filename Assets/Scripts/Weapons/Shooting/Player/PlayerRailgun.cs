using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRailgun : PlayerShooting
{  
    ParticleSystem chargeLight;
    public float chargeDuration;
    AudioSource chargeSound;

    public GameObject prefabOfShot;



    void Start()
    {
        source = GetComponentInParent<EntityHandler>();
        muzzle = transform.Find("muzzle");

        shotDelegate = Shot;

        inputActions = new();
        if (!GameHandler.instance.GameIsPaused) inputActions.PlayerTankControl.Enable();

        shotSound = GetComponent<AudioSource>();
        chargeSound = transform.Find("ChargeSound").GetComponent<AudioSource>();

        chargeLight = transform.Find("muzzleFlash").GetComponent<ParticleSystem>();

        enemyMask = source.EnemiesMasks + LevelHandler.instance.groundlayers;

        remainingDelay = 0;
    }

    
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
            shotDelegate();
        }
    }

    protected override void Shot()
    {
        StartCoroutine(ShotCoroutine());
    }
    IEnumerator ShotCoroutine()
    {
        remainingDelay = delayBetweenShots;
        chargeSound.Play();
        chargeLight.Play();

        yield return new WaitForSeconds(chargeDuration);
        chargeSound.Stop();
        chargeLight.Stop();

        shotSound.Play();

        
        if (Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hit, weapRange, enemyMask))
        {
            //Debug.Log(hit.collider);

            IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {
                //print(damagable);
                if (!damagable.IsDead)
                {
                    damagable.DealDamage(new Damage(damage, source)) ;
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

