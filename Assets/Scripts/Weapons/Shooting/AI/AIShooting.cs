using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : Shooting
{
    public EntityHandler source;
    public float damage;

    protected bool isStunned;

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
