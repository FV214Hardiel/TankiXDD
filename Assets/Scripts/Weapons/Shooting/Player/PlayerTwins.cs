using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwins : PlayerShooting
{
    public GameObject shellPref;
    public float shellVeloc;

    public float delayBetweenShots;
    float remainingDelay;

    public Transform muzzleL;
    public Transform muzzleR;

    bool shotFromRight;

    float debuffPower;
    float debuffDuration;

    AudioSource shotSound;

    

    void Start()
    {
        debuffPower = 3;
        debuffDuration = 10;

        source = GetComponentInParent<EntityHandler>().gameObject;

        shotSound = GetComponent<AudioSource>();

        remainingDelay = 0;
        shotFromRight = true;

        muzzleL = transform.Find("muzzleL");
        muzzleR = transform.Find("muzzleR");


    }


    void Update()
    {
        if (GameHandler.GameIsPaused) return; //Checking pause

        if (remainingDelay > 0)  remainingDelay -= Time.deltaTime;                

        if (Input.GetButton("Fire1") && remainingDelay <= 0) //Shot
        {
            remainingDelay = delayBetweenShots;
            if (shotFromRight) //Checking for barrel
            {
                TwinsShell.CreateShot(shellPref, muzzleR.position, muzzleR.forward * shellVeloc, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration));
                shotFromRight = false;
                shotSound.Play();
            }
            else
            {
                TwinsShell.CreateShot(shellPref, muzzleL.position, muzzleL.forward * shellVeloc, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration));
                shotFromRight = true;
                shotSound.Play();
            }         

        }

    }
}
