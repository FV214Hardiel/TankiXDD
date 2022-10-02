using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIVulcan : AIShooting
{
    public float delayBetweenShots;
    float remainingDelay;

    [SerializeField] byte stacks;
    public float stackDuration;
    float stackTimer;
    float modifiedDelay;

    public Transform muzzle;

    public float weapRange;

    Vector3 shotVector;
    public float angle;

    List<ushort> disperseAngles;
    List<float> disperseLengths;
    int index;

    AudioSource shotSound;
    AudioSource chargeSound;

    public GameObject prefabOfShot;
    public ParticleSystem shotEffect;

    AIMove ai;
    bool isTargetLocked;

    Ray lineOfFire;
    RaycastHit hit;
    LayerMask enemyMask;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponentInParent<EntityHandler>();
        muzzle = transform.Find("muzzle");

        ai = gameObject.GetComponentInParent<AIMove>();
        enemyMask = ai.enemyLayers;

        shotSound = GetComponent<AudioSource>();
        chargeSound = transform.Find("ChargesSound").GetComponent<AudioSource>();

        remainingDelay = 0;

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

        stacks = 0;
        stackTimer = 0;

        StartCoroutine(CustomUpdate(0.3f));
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
        if (GameHandler.GameIsPaused) return; //Checking pause

        chargeSound.pitch = stacks > 0 ? (0.4f + stacks * 0.03f) : 0;

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }
        //Stacks are decreasing with time
        stackTimer -= Time.deltaTime;
        if (stackTimer <= 0 && stacks > 0)
        {
            stacks--;
            stackTimer = stackDuration;
        }


        if (isTargetLocked) //Shot
        {
            shotVector = DisperseVector(muzzle.forward, angle);
            Shot(shotVector);
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

    void Shot(Vector3 shotVector)
    {
        shotSound.Play();
        shotEffect.Play();

        if (Physics.Raycast(muzzle.position, shotVector, out RaycastHit hit, weapRange))
        {
            EntityHandler eh = hit.collider.GetComponentInParent<EntityHandler>(false);
            if (eh != null)
            {
                if (!eh.isDead) //Checking if target is alive and wasnt already hit by this shot
                {
                    eh.DealDamage(damage, source);
                }
            }

            WeaponTrail.Create(prefabOfShot, muzzle.position, hit.point); //Shot VFX if hit
        }
        else
        {
            WeaponTrail.Create(prefabOfShot, muzzle.position, muzzle.position + shotVector * weapRange); //Shot VFX if no hit
        }

        //Increasing Attack Speed
        modifiedDelay = (delayBetweenShots * 1.8f) / (stacks + 1 + 0.8f);
        remainingDelay = modifiedDelay;

        stacks += 1;
        stacks = (byte)(Mathf.Clamp(stacks, 0, 17));
        stackTimer = stackDuration;
    }

    private void OnDisable()
    {
        shotSound.Stop();
        chargeSound.Stop();
    }
}
