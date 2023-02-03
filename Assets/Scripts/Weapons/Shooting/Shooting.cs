using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public IEntity source;

    public delegate void ShotDelegate();
    public ShotDelegate shotDelegate;

    public float weapRange;
    public float damage;
    protected float shotDamage;

    protected Transform muzzle;

    protected AudioSource shotSound;

    public float delayBetweenShots;
    protected float remainingDelay;

    protected bool isStunned;

    protected LayerMask enemyMask;
    protected LayerMask friendlyMask;

    public Func<float, float> outputDamageModifiers;

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
}
