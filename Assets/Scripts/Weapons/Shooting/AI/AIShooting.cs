using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : Shooting
{
      

   
    protected Vector3 shotVector;

    public float angle;
    protected List<ushort> disperseAngles;
    protected List<float> disperseLengths;
    protected int index;

    

   

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
