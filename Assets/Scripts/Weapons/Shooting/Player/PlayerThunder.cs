using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThunder : PlayerShooting
{
    public GameObject shellPref;   
    public GameObject trailPref;

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

        shotDelegate = Shot;

        inputActions = new();
        
            inputActions.PlayerTankControl.Enable();
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

        //Making a shot
        if (inputValue > 0)
        {
            shotDelegate();
        }

    }

    protected override void Shot()
    {
        shotSound.Play();
        ThunderShell.CreateShot(shellPref, muzzle.position, muzzle.forward * projectileSpeed, source, damage, timeOfLife);
        remainingDelay = delayBetweenShots;
    }

    protected override void OverloadShot()
    {
        shotSound.Play();
        ThunderShell.CreatePellets(trailPref, muzzle.position, muzzle.forward, source, pelletsCount, pelletDamage, pelletsAngle, pelletsDistance, muzzle.up);
        remainingDelay = delayBetweenShotsEnhanced;
        
    }
}
