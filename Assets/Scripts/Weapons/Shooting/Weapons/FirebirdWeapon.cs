using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebirdWeapon : Weapon
{   
    public GameObject prefab;

    public float projectileSpeed;
    float timeOfLife;

    new void Start()
    {
       
        base.Start();

        timeOfLife = weapRange / projectileSpeed;
        
        //shotSound.pitch = 0;

        InitAnglesAndLengthLists();
    }

    public override void OpenFire()
    {
        isOpenFire = true;
        if (remainingDelay <= 0 && !isStunned)
        {
            shotSound.Play();
            shotDelegate();
        }
    }

    public override void CeaseFire()
    {
        shotSound.Stop();
        isOpenFire = false;
    }

    public override void Stun()
    {
        base.Stun();
        shotSound.Stop();
    }

    public override void UnStun()
    {
        isStunned = false;
        if (isOpenFire && remainingDelay <= 0)
        {
            shotSound.Play();
            shotDelegate();
        }

        
    }

    //void Update()
    //{
    //    if (GameHandler.instance.GameIsPaused) //Checking pause
    //    {
    //        shotSound.pitch = 0;
    //        return;
    //    }

    //    inputValue = inputActions.PlayerTankControl.Fire.ReadValue<float>();

    //    shotSound.pitch = inputValue;

    //    if (remainingDelay > 0) //Decreasing delay timer          
    //    {
    //        remainingDelay -= Time.deltaTime;
    //        return; 
    //    }

    //    //Making a shot
    //    if (inputValue > 0)
    //    {
    //        Shot();
    //    }

    //}

    protected override void Shot()
    {
        shotVector = DisperseVector(muzzle.forward, angle) * projectileSpeed;

        FirebirdShot.CreateShot(prefab, muzzle.position, shotVector, source, damage, timeOfLife);

        remainingDelay = delayBetweenShots;
    }
    


}
