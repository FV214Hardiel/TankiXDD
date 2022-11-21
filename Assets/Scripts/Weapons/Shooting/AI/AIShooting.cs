using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : Shooting
{
    public EntityHandler source;

    public float weapRange;
    public float damage;

    public float angle;
    protected Vector3 shotVector;

    protected List<ushort> disperseAngles;
    protected List<float> disperseLengths;
    protected int index;

    protected Transform muzzle;

    protected AudioSource shotSound;

    public float delayBetweenShots;
    protected float remainingDelay;

    protected bool isStunned;

    protected AIMove ai;
    public bool isTargetLocked;

    protected Ray lineOfFire;
    protected RaycastHit hit;
    

    protected virtual void OnStun()
    {       
        isStunned = true;
        StopAllCoroutines();

    }
    protected virtual void OnUnStun()
    {
        isStunned = false;
    }
}
