using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITwins : AIShooting

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
    GameObject ShellFired;

    AudioSource shotSound;

    Ray lineOfFire;
    RaycastHit hit;



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
        if (GameHandler.GameIsPaused) return;
        remainingDelay -= Time.deltaTime;

        lineOfFire = new Ray(muzzleL.position, muzzleL.forward);

        Physics.Raycast(lineOfFire, out hit);

        if ((hit.collider == Player.PlayerHullColl || hit.collider == Player.PlayerTurretColl) && remainingDelay <= 0)
        {
            remainingDelay = delayBetweenShots;
            if (shotFromRight)
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
