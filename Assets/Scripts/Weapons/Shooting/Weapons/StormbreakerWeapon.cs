using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StormbreakerWeapon : Weapon
{
    AudioSource chargingSound;
    AudioSource chargedSound;

    bool isCharging;
    
    float chargeEnergy;

    public float maxCharge;

    Light chargeLight;
    //float testTimer;

    public LayerMask affectedLayers;
    public LayerMask stoppingLayers;

    public GameObject prefabOfShot;
    public ParticleSystem shotEffect;

    new void OnEnable()
    {
        base.OnEnable();

        chargeLight = transform.Find("ChargeLight").GetComponent<Light>();

        chargingSound = transform.Find("ChargingSound").GetComponent<AudioSource>();
        chargedSound = transform.Find("ChargedSound").GetComponent<AudioSource>();
    }

    new void Start()
    {
        base.Start();

        friendlyMask = source.FriendlyMasks;
        enemyMask = source.EnemiesMasks;       
        affectedLayers += enemyMask;
        chargeEnergy = 0;       
    }

    new void Update()
    {
        if (GameHandler.instance.GameIsPaused) return; //Checking pause

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            if (remainingDelay <= 0 && isOpenFire && !isStunned) StartCharge();
        }

        if (isCharging)
        {
            //testTimer += Time.deltaTime;
            chargeEnergy += Time.deltaTime * 10;
            chargeLight.intensity = chargeEnergy / maxCharge;
            if (chargeEnergy >= maxCharge)
            {
                //print(testTimer);
                
                isCharging = false;
                chargedSound.Play();
                chargeLight.intensity = 3;

                if (!source.isPlayer) Invoke(nameof(StopCharge), 1f); //fix for AI who stuck on charge
            }
        }

    }

    protected override void Shot()
    {               
        shotSound.Play();
        shotEffect.Play();

        Ray ray = new(muzzle.position, muzzle.forward);

        RaycastHit[] hit = Physics.RaycastAll(ray, weapRange, affectedLayers);

        List<RaycastHit> newList = new();
        newList.AddRange(hit);

        Vector3 endOfLine = transform.position + transform.forward * weapRange;
        newList.Sort((x, y) => x.distance.CompareTo(y.distance));

        List<IEntity> hitList = new();

        foreach (RaycastHit item in newList)
        {
            IDamagable damagable = item.collider.GetComponent<IDamagable>();
            if (damagable != null)
            {
                if (!hitList.Contains(damagable.Entity))
                {
                    damagable.DealDamage(new Damage(damage, source));
                    hitList.Add(damagable.Entity);
                }                

            }

            if (item.transform.gameObject.layer == stoppingLayers)
            {
                endOfLine = item.point;
                break;
            }
        }

        WeaponTrail.Create(prefabOfShot, muzzle.position, endOfLine);
        //print(damage);
        remainingDelay = delayBetweenShots;

    }

    protected override void OverloadShot()
    {
        shotSound.Play();
        shotEffect.Play();

        Ray ray = new(muzzle.position, muzzle.forward);

        RaycastHit[] hit = Physics.RaycastAll(ray, weapRange, affectedLayers);

        List<RaycastHit> newList = new();
        newList.AddRange(hit);

        Vector3 endOfLine = transform.position + transform.forward * weapRange;
        newList.Sort((x, y) => x.distance.CompareTo(y.distance));

        List<IEntity> hitList = new();

        byte mult = 0;

        foreach (RaycastHit item in newList)
        {
            IDamagable damagable = item.transform.GetComponent<IDamagable>();

            if (damagable != null)
                if (!hitList.Contains(damagable.Entity))
                {
                   mult++;
                    hitList.Add(damagable.Entity);
                }
        }

        hitList = new();
        foreach (RaycastHit item in newList)
        {
            IDamagable damagable = item.collider.GetComponent<IDamagable>();

            if (damagable != null)
            {
                if (!hitList.Contains(damagable.Entity))
                {
                    damagable.DealDamage(new Damage(damage*mult, source));
                    hitList.Add(damagable.Entity);
                }
            }            

            if (item.transform.gameObject.layer == stoppingLayers)
            {
                endOfLine = item.point;
                break;
            }
        }
        print(mult);
        print(damage * mult);
        WeaponTrail.Create(prefabOfShot, muzzle.position, endOfLine);

        remainingDelay = delayBetweenShots;
    }



    //private void OnDestroy()
    //{
    //    inputActions.PlayerTankControl.Fire.started -= FireButtonStarted;
    //    inputActions.PlayerTankControl.Fire.canceled -= FireButtonCanceled;
    //}

    public override void OpenFire()
    {
        if (isOpenFire) return;

        isOpenFire = true;
        if (remainingDelay > 0 || isStunned) return;

        StartCharge();

        
    }

    public override void CeaseFire()
    {
        if (!isOpenFire) return;

        isOpenFire = false;
        StopCharge();
    }

    void StartCharge()
    {
        isCharging = true;
        chargingSound.Play();
    }

    void StopCharge()
    {
        chargingSound.Stop();
        isCharging = false;
        if (chargeEnergy >= maxCharge)
        {
            chargedSound.Stop();
            shotDelegate();

        }
        chargeEnergy = 0;
        chargeLight.intensity = 0;
    }

    //void FireButtonStarted(InputAction.CallbackContext context)
    //{
    //    if (remainingDelay > 0) return;
        
    //    isCharging = true;
    //    chargingSound.Play();
    //}

    //void FireButtonCanceled(InputAction.CallbackContext context)
    //{

    //    chargingSound.Stop();
    //    isCharging = false;
    //    if (chargeEnergy >= maxCharge)
    //    {
    //        chargedSound.Stop();
    //        shotDelegate();            
            
    //    }
    //    chargeEnergy = 0;
    //    testTimer = 0;
    //    chargeLight.intensity = 0;
    //    //print(testTimer);
    //}
}
