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

        source = GetComponentInParent<EntityHandler>();
        muzzleL = transform.Find("muzzleL");
        muzzleR = transform.Find("muzzleR");

        ai = gameObject.GetComponentInParent<AIMove>();
        enemyMask = ai.enemyLayers;

        shotSound = GetComponent<AudioSource>();

        timeOfLife = weapRange / projectileSpeed;

        remainingDelay = 0;
        shotFromRight = true;


        debuffPower = 3;
        debuffDuration = 10;

        StartCoroutine(CustomUpdate(0.3f));


    }

    public IEnumerator CustomUpdate(float timeDelta)
    {
        while (true)
        {
            if (ai.AIState == AIMove.AIEnum.Attack)
            {
                lineOfFire = new Ray(muzzleL.position, muzzleL.forward);
                isTargetLocked = Physics.Raycast(lineOfFire, weapRange, enemyMask);
            }
            else
            {
                isTargetLocked = false;
            }

            yield return new WaitForSeconds(timeDelta);
        }
    }

    void Update()
    {
        if (GameHandler.GameIsPaused) return; //Checking pause

        if (remainingDelay > 0) //Checking and decreasing weapon CD
        {
            remainingDelay -= Time.deltaTime;
            return;
        }        

        if (isTargetLocked) //Shot
        {            
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

            remainingDelay = delayBetweenShots;

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
