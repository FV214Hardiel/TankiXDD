using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    public IEntity source;

    protected bool isOpenFire;

    public delegate void ShotDelegate();
    public ShotDelegate shotDelegate;

    public delegate void HitDelegate(IDamagable hit, Damage dmg);
    public HitDelegate hitDelegate;

    public float weapRange;
    public float additionalRange;
    public float damage;
    protected float shotDamage;

    public Transform muzzle;

    protected AudioSource shotSound;

    public float delayBetweenShots;
    protected float remainingDelay;

    protected bool isStunned;

    
    public LayerMask enemyMask;
    public LayerMask friendlyMask;

    public Func<float, float> outputDamageModifiers;

    protected Vector3 shotVector;

    public float angle;
    protected List<ushort> disperseAngles;
    protected List<float> disperseLengths;
    protected int index;

    protected PlayerInputActions inputActions;
    protected float inputValue;

    protected void OnEnable()
    {
        shotSound = GetComponent<AudioSource>();

        remainingDelay = 0;

        shotDelegate = Shot;

        muzzle = transform.Find("muzzle");
    }

    protected void Start()
    {
        //Base setup for shooting
        source = GetComponentInParent<TankEntity>(); 
        enemyMask = source.EnemiesMasks;
        source.EntityStunned += Stun;
        source.EntityAwaken += UnStun;

        if (source.isPlayer)
        {
            inputActions = new();

            inputActions.PlayerTankControl.Enable();

            inputActions.PlayerTankControl.Fire.started += (o) => OpenFire();
            inputActions.PlayerTankControl.Fire.canceled += (o) => CeaseFire();
        }

    }

    protected void Update()
    {
        if (GameHandler.instance.GameIsPaused) return; //Checking pause

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            if (remainingDelay <= 0 && isOpenFire && !isStunned) shotDelegate();

        }
    }

    protected virtual void Shot()
    {
        shotDamage = damage;
        if (outputDamageModifiers != null)
        {
            foreach (Func<float, float> item in outputDamageModifiers.GetInvocationList())
            {
                shotDamage = item(shotDamage);
            }
        }
    }

    protected virtual void OverloadShot()
    {
        shotDamage = damage;
        if (outputDamageModifiers != null)
        {
            foreach (Func<float, float> item in outputDamageModifiers.GetInvocationList())
            {
                shotDamage = item(shotDamage);
            }
        }
    }

    public virtual void EnableOverload()
    {
        shotDelegate = OverloadShot;
    }

    public virtual void DisableOverload()
    {
        shotDelegate = Shot;
    }

    public virtual void Stun()
    {
        isStunned = true;
    }

    public virtual void UnStun()
    {
        isStunned = false;
        if (isOpenFire && remainingDelay <= 0) shotDelegate();

    }

    protected virtual void BasicHit(IDamagable hit, Damage dmg)
    {

    }

    public virtual void OpenFire() 
    {
        isOpenFire = true;
        if (remainingDelay <= 0 && !isStunned)
        {
            shotDelegate();
        }
    }

    public virtual void CeaseFire() => isOpenFire = false;

    protected void InitAnglesAndLengthLists()
    {
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

    protected Vector3 DisperseVector(Vector3 originalVector, float angle)
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
        vector += Quaternion.AngleAxis(angleDis, originalVector) * (lenghtDis * ratioMultiplier * transform.up);

        return vector.normalized;
    }
}
