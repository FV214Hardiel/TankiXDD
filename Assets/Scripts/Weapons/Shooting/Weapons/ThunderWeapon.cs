using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThunderWeapon : Weapon
{
    public GameObject shellPref;   
    public GameObject trailPref;
    
    public float expRadius;
    public float expDamage;

    public int pelletsCount;
    public float pelletsDistance;
    public float pelletsAngle;
    public float pelletDamage;

    public float delayBetweenShotsEnhanced;

    public float projectileSpeed;
    float timeOfLife;
    void Start()
    {
        //Base setup for shooting
        source = GetComponentInParent<TankEntity>();
        muzzle = transform.Find("muzzle");

        timeOfLife = weapRange / projectileSpeed;

        shotSound = GetComponent<AudioSource>();

        remainingDelay = 0;

        enemyMask = source.EnemiesMasks;

        InitAnglesAndLengthLists();

        shotDelegate = Shot;

        source.EntityStunned += Stun;
        source.EntityAwaken += UnStun;

        if (source.isPlayer)
        {
            inputActions = new();

            inputActions.PlayerTankControl.Enable();

            inputActions.PlayerTankControl.Fire.started += (o) => OpenFire();
            inputActions.PlayerTankControl.Fire.canceled += (o) => CeaseFire();
        }        

    }

    public override void OpenFire()
    {
        
        isOpenFire = true;
        if (remainingDelay <= 0 && !isStunned)
        {
            shotDelegate();
        }
    }

    public override void CeaseFire()
    {
        isOpenFire = false;
    }

    public override void Stun()
    {
        isStunned = true;
    }

    public override void UnStun()
    {
        isStunned = false;
        if (isOpenFire && remainingDelay <= 0) shotDelegate();
    }

    void Update()
    {       

        if (GameHandler.instance.GameIsPaused) return; //Checking pause

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            if ( remainingDelay <= 0 && isOpenFire && !isStunned)  shotDelegate();
            
        }

    }

    protected override void Shot()
    {
        shotSound.Play();
        //ThunderShell.CreateShot(shellPref, muzzle.position, muzzle.forward * projectileSpeed, source, damage, timeOfLife);
        CreateShell();
        remainingDelay = delayBetweenShots;
    }

    protected override void OverloadShot()
    {
        shotSound.Play();
        //ThunderShell.CreatePellets(trailPref, muzzle.position, muzzle.forward, source, pelletsCount, pelletDamage, pelletsAngle, pelletsDistance, muzzle.up);
        CreatePellets();
        remainingDelay = delayBetweenShotsEnhanced;
        
    }

    void CreateShell()
    {
        GameObject go = Instantiate(shellPref, muzzle.position, Camera.main.transform.rotation);
        go.GetComponent<Rigidbody>().velocity = muzzle.forward * projectileSpeed;
        ThunderShell sht = go.GetComponent<ThunderShell>();
        sht.expDamage = expDamage;
        sht.expRadius = expRadius;
        sht.source = source;
        sht.timer = timeOfLife;

        sht.pelletDamage = pelletDamage;
        sht.pelletsAngle    = pelletsAngle;
        sht.pelletsCount = pelletsCount;
        sht.pelletsDistance = pelletsDistance;

    }

    void CreatePellets()
    {
        Vector3 shotVector;

        RaycastHit hit;
        for (int i = 0; i < pelletsCount; i++)
        {
            shotVector = DisperseVector(muzzle.forward, pelletsAngle);

            if (Physics.Raycast(muzzle.position, shotVector, out hit, pelletsDistance))
            {
                WeaponTrail.Create(trailPref, muzzle.position, hit.point);

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
                WeaponTrail.Create(trailPref, muzzle.position, muzzle.position + shotVector * pelletsDistance); //Shot VFX if no hit
            }

        }
    }
}
