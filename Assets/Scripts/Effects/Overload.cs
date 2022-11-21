using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overload : Effect
{
    public Overload(float duration)
    {
        effectDuration = duration;
        
        remainingDuration = duration;

        effectID = "overload";

        isEffectStackable = false;
        isTimeStackable = false;




    }
    public override float Tick(float deltatime)
    {
        remainingDuration -= deltatime;
        if (remainingDuration <= 0)
        {
            remainingDuration = 0;
            EndEffect();

        }

        return remainingDuration;
    }

    public override void InitEffect()
    {
        affectedObject.Gameobject.GetComponentInChildren<Shooting>().EnableOverload();
    }   
   

    public override void EndEffect()
    {
        affectedObject.Gameobject.GetComponentInChildren<Shooting>().DisableOverload();
    }
}
