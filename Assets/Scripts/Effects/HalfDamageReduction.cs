using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDamageReduction : Effect
{
    public HalfDamageReduction(float effDur)
    {
        effectID = 4;

        effectDuration = effDur;
        remainingDuration = effectDuration;

        isEffectStackable = false;
        isTimeStackable = false;

        canBeDifferentInstances = false;
        

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
        affectedObject.receivingDamageEffects += HalfApplyEffect;
        Debug.Log("HalfReduction started");
    }

    public float HalfApplyEffect(float damage)
    {
        return damage / 2;
    }

    public override void EndEffect()
    {
        affectedObject.receivingDamageEffects -= HalfApplyEffect;
        Debug.Log("HalfReduction stopped");
    }
}
