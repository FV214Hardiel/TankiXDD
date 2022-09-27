using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITwins : AIShooting

{
    public GameObject shellPref;
   

    public float delayBetweenShots;
    float remainingDelay;

    public Transform muzzleL;
    public Transform muzzleR;

    bool shotFromRight;

    float debuffPower;
    float debuffDuration;
    GameObject ShellFired;

    public float weapRange;
    public float projectileSpeed;
    float timeOfLife;

    AudioSource shotSound;

    AIMove ai;
    bool isTargetLocked;

    Ray lineOfFire;
    RaycastHit hit;
    LayerMask enemyMask;



    void Start()
    {


        debuffPower = 3;
        debuffDuration = 10;

        source = GetComponentInParent<EntityHandler>().gameObject;
        ai = gameObject.GetComponentInParent<AIMove>();

        shotSound = GetComponent<AudioSource>();

        timeOfLife = weapRange / projectileSpeed;

        remainingDelay = 0;
        shotFromRight = true;

        muzzleL = transform.Find("muzzleL");
        muzzleR = transform.Find("muzzleR");


    }


    void Update()
    {
        if (GameHandler.GameIsPaused) return;
        if (remainingDelay > 0)
        {
            remainingDelay -= Time.deltaTime;
            return;
        }
        

        lineOfFire = new Ray(muzzleL.position, muzzleL.forward);

        //Physics.Raycast(lineOfFire, out hit, );

        if (ai.AIState == AIMove.AIEnum.Attack)
        {
            remainingDelay = delayBetweenShots;
            if (shotFromRight)
            {
                TwinsShell.CreateShot(shellPref, muzzleR.position, muzzleR.forward * projectileSpeed, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration), timeOfLife);
                shotFromRight = false;
                shotSound.Play();
            }
            else
            {
                TwinsShell.CreateShot(shellPref, muzzleL.position, muzzleL.forward * projectileSpeed, source, damage, new TwinsDamageStacks(debuffPower, debuffDuration), timeOfLife);
                shotFromRight = true;
                shotSound.Play();
            }




        }

    }
}

//{
//    public GameObject shellPref;
//    public float shellVeloc;
//    public float reloadTime;
//    float nextShot;
//    public Transform muzzleL;
//    public Transform muzzleR;
//    bool shotFromRight;
//    GameObject ShellFired;
//    AIMove hull;
//    [SerializeField]
//    Transform gun;

//    Ray lineOfFire;
//    RaycastHit hit;
//    //bool isPlayer;

//    void Start()
//    {
//        nextShot = Time.time;
//        shotFromRight = true;
//        hull = gameObject.GetComponentInParent<AIMove>();
//        gun = transform.Find("barrel");


//    }


//    void Update()
//    {

//        lineOfFire = new Ray(gun.position, gun.forward);
//        Debug.DrawRay(gun.position, gun.forward*10f);
//        Physics.Raycast(lineOfFire, out hit);

//        if ((hit.collider == Player.PlayerHullColl || hit.collider == Player.PlayerTurretColl) && Time.time > nextShot)
//        {
//            nextShot = Time.time + reloadTime;
//            if (shotFromRight)
//            {
//                ShellFired = Instantiate(shellPref, muzzleR.position + transform.forward, transform.rotation);
//                ShellFired.GetComponent<Rigidbody>().velocity = muzzleR.forward * shellVeloc;
//                shotFromRight = false;
//            }
//            else
//            {
//                ShellFired = Instantiate(shellPref, muzzleL.position + transform.forward, transform.rotation);
//                ShellFired.GetComponent<Rigidbody>().velocity = muzzleL.forward * shellVeloc;
//                shotFromRight = true;
//            }


//            //Debug.Log("SOZDAN");
//            Destroy(ShellFired, 1);

//        }

//    }
//}
