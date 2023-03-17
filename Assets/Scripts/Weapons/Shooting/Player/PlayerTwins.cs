using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTwins : PlayerShooting
{
    public GameObject shellPref;

    public Transform muzzleL;
    public Transform muzzleR;
    bool shotFromRight;

    float debuffPower;
    float debuffDuration;

    public float projectileSpeed;
    float timeOfLife;

    public bool test;

    void Start()
    {

       
        //Base setup for shooting
        source = GetComponentInParent<TankEntity>();

        timeOfLife = weapRange / projectileSpeed;

        shotSound = GetComponent<AudioSource>();

        remainingDelay = 0;

        //Delegate goes on
        shotDelegate = Shot;

        inputActions = new();
       // if (!GameHandler.instance.GameIsPaused) 
            inputActions.PlayerTankControl.Enable();

        //Specific setup
        debuffPower = 3;
        debuffDuration = 10;

        shotFromRight = true;

        muzzleL = transform.Find("muzzleL");
        muzzleR = transform.Find("muzzleR");

    }


    void Update()
    {
        //print("update start");
        if (GameHandler.instance.GameIsPaused) return; //Checking pause

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }

        inputValue = inputActions.PlayerTankControl.Fire.ReadValue<float>();

        //print(inputValue);
       // print(inputActions);
       // print(inputActions.PlayerTankControl);

        //print("read value");
        //Making a shot
        if (inputValue > 0) 
        {
            
            shotDelegate();           

        }

    }

    protected override void Shot()
    {

       // print("shot");
        remainingDelay = delayBetweenShots;

        if (shotFromRight) //Checking for barrel
        {
            TwinsShell.CreateShot(shellPref, muzzleR.position, muzzleR.forward * projectileSpeed, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration), timeOfLife);
            shotFromRight = false;
            
        }
        else
        {
            TwinsShell.CreateShot(shellPref, muzzleL.position, muzzleL.forward * projectileSpeed, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration), timeOfLife);
            shotFromRight = true;
            
        }

        
        shotSound.PlayOneShot(shotSound.clip);
    }

    protected override void OverloadShot()
    {
        print("overshot");
        remainingDelay = delayBetweenShots;

        TwinsShell.CreateShot(shellPref, muzzleR.position, muzzleR.forward * projectileSpeed, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration), timeOfLife);
        TwinsShell.CreateShot(shellPref, muzzleL.position, muzzleL.forward * projectileSpeed, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration), timeOfLife);

        shotSound.PlayOneShot(shotSound.clip);
        //shotSound.Play();

    }
}
//{
//    public GameObject shellPref;
//    public float shellVeloc;

//    public float delayBetweenShots;
//    float remainingDelay;

//    public Transform muzzleL;
//    public Transform muzzleR;

//    bool shotFromRight;

//    float debuffPower;
//    float debuffDuration;

//    AudioSource shotSound;



//    void Start()
//    {
//        debuffPower = 3;
//        debuffDuration = 10;

//        source = GetComponentInParent<EntityHandler>().gameObject;

//        shotSound = GetComponent<AudioSource>();

//        remainingDelay = 0;
//        shotFromRight = true;

//        muzzleL = transform.Find("muzzleL");
//        muzzleR = transform.Find("muzzleR");


//    }


//    void Update()
//    {
//        if ((GameHandler.instance.GameIsPaused) return; //Checking pause

//        if (remainingDelay > 0)  remainingDelay -= Time.deltaTime;

//        if (Input.GetButton("Fire1") && remainingDelay <= 0) //Shot
//        {
//            remainingDelay = delayBetweenShots;
//            if (shotFromRight) //Checking for barrel
//            {
//                TwinsShell.CreateShot(shellPref, muzzleR.position, muzzleR.forward * shellVeloc, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration));
//                shotFromRight = false;
//                shotSound.Play();
//            }
//            else
//            {
//                TwinsShell.CreateShot(shellPref, muzzleL.position, muzzleL.forward * shellVeloc, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration));
//                shotFromRight = true;
//                shotSound.Play();
//            }

//        }

//    }


//}
