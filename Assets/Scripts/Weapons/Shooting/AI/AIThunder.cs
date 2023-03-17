using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIThunder : AIShooting
{
    public GameObject shellPref;

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

        ai = gameObject.GetComponentInParent<AIMove>();
        enemyMask = ai.enemyLayers;

        source.EntityStunned += OnStun;
        source.EntityAwaken += OnUnStun;

        StartCoroutine(CustomUpdate(0.3f));
    }
    private void OnDisable()
    {
        source.EntityStunned -= OnStun;
        source.EntityAwaken -= OnUnStun;

    }

    protected override void OnStun()
    {
        base.OnStun();
    }

    protected override void OnUnStun()
    {
        base.OnUnStun();
        StartCoroutine(CustomUpdate(1));
    }

    public IEnumerator CustomUpdate(float timeDelta)
    {
        while (true)
        {
            if (ai.AIState == AIMove.AIEnum.Attack)
            {
                lineOfFire = new Ray(muzzle.position, muzzle.forward);
                isTargetLocked = Physics.Raycast(lineOfFire, weapRange + 40, enemyMask);
            }
            else
            {
                isTargetLocked = false;
            }
            //print(isTargetLocked);
            yield return new WaitForSeconds(timeDelta);
        }
    }

    private void Update()
    {
        if (GameHandler.instance.GameIsPaused || isStunned) //Checking pause
        {            
            return;
        }

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }

        if (isTargetLocked) //Shot
        {
            Shot();
        }
    }

    protected override void Shot()
    {
        shotSound.Play();
        ThunderShell.CreateShot(shellPref, muzzle.position, muzzle.forward * projectileSpeed, source, damage, timeOfLife);
        remainingDelay = delayBetweenShots;
    }
}
