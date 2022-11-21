using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFirebird : AIShooting
{
    public GameObject shellPrefab;
   
    
    public float projectileSpeed;
    float timeOfLife;

   
    void Start()
    {
        source = GetComponentInParent<EntityHandler>();
        muzzle = transform.Find("muzzle");

        ai = gameObject.GetComponentInParent<AIMove>();
        enemyMask = ai.enemyLayers;

        shotSound = GetComponent<AudioSource>();

        timeOfLife = weapRange / projectileSpeed;

        remainingDelay = 0;

        source.TankStunned += OnStun;
        source.TankAwaken += OnUnStun;

        StartCoroutine(CustomUpdate(0.3f));

        shotSound = GetComponent<AudioSource>();
        shotSound.pitch = 0;

        disperseAngles = new();
        for (int _ = 0; _ < 50; _++)
        {
            disperseAngles.Add((ushort)Random.Range(1, 357));
        }

        disperseLengths = new();
        for (int _ = 0; _ < 50; _++)
        {
            disperseLengths.Add(Random.Range(0f, 1f));
        }

        index = 0;
    }

    private void OnDisable()
    {
        source.TankStunned -= OnStun;
        source.TankAwaken -= OnUnStun;
        shotSound.Stop();

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
                isTargetLocked = Physics.Raycast(lineOfFire, weapRange, enemyMask);
            }
            else
            {
                isTargetLocked = false;
            }

            yield return new WaitForSeconds(timeDelta);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.instance.GameIsPaused || isStunned) //Checking pause
        {
            shotSound.pitch = 0;
            return;
        }

        shotSound.pitch = isTargetLocked ? 1 : 0;

        if (remainingDelay > 0) //Decreasing delay timer          
        {
            remainingDelay -= Time.deltaTime;
            return;
        }

        if (isTargetLocked)
        {
            shotVector = DisperseVector(muzzle.forward, angle) * projectileSpeed;

            FirebirdShot.CreateShot(shellPrefab, muzzle.position, shotVector, source, damage, timeOfLife);

            remainingDelay = delayBetweenShots;
        }
    }

    Vector3 DisperseVector(Vector3 originalVector, float angle)
    {

        Vector3 vector = originalVector.normalized; //Original vector must be normalized

        //Taking random values from pregenerated lists
        ushort angleDis = disperseAngles[index];
        float lenghtDis = disperseLengths[index];
        index++;
        if (index >= disperseAngles.Count) index = 0; //Cycling indexes

        angle *= Mathf.Deg2Rad; //Angle from degrees to rads
        float ratioMultiplier = Mathf.Tan(angle); //Tangens of angle for ratio between Dispersion Leg and Base Leg

        //Adding UP vector multiplied by ratio and random value and rotated on random angle
        vector += Quaternion.AngleAxis(angleDis, originalVector) * (lenghtDis * ratioMultiplier * muzzle.up);

        return vector.normalized;
    }

    
}
