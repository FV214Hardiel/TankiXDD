using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailgunWeapon : Weapon
{  
    ParticleSystem chargeLight;
    public float chargeDuration;
    AudioSource chargeSound;

    public GameObject prefabOfShot;


    new void Start()
    {
        base.Start();
        
        chargeSound = transform.Find("ChargeSound").GetComponent<AudioSource>();

        chargeLight = transform.Find("muzzleFlash").GetComponent<ParticleSystem>();

        enemyMask = source.EnemiesMasks + LevelHandler.instance.groundlayers;

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

