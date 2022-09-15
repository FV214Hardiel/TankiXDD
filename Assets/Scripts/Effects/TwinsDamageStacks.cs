using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinsDamageStacks : Effect
{
    
    //public TwinsDamageStacks(float duration, float power, GameObject obj)
    //{
    //    effectDuration = duration;
    //    effectPower = power;
    //    remainingDuration = duration;

    //    affectedObject = obj;

    //    effectID = 2;

    //    isEffectStackable = true;

    //    effectStacks = 1;
    //}

    public TwinsDamageStacks(float duration, float power)
    {
        effectDuration = duration;
        effectPower = power;
        remainingDuration = duration;

        //affectedObject = null;

        effectID = 2;

        isEffectStackable = true;

        effectStacks = 1;
    }
    public override void InitEffect()
    {

    }

    public override float Tick(float deltatime)
    {
        //Debug.Log("TICK");
        remainingDuration -= deltatime;
        if (remainingDuration <= 0)
        {
            remainingDuration = 0;
            effectStacks = 0;

        }


        return remainingDuration;
    }

}
