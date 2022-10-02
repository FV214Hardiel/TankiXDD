using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesDamageReduction : Effect
{
    // Start is called before the first frame update
    public PiecesDamageReduction(float effDur)
    {

        effectID = 3;
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
        affectedObject.receivingDamageEffects += PiecesApplyEffect;
        Debug.Log("Pieces started");
    }

    public float PiecesApplyEffect(float damage)
    {
        if (damage > 20)
        {
            return damage - 10;
        }
        else
        {
            return (damage / 2);
        }


    }

    public override void EndEffect()
    {
        affectedObject.receivingDamageEffects -= PiecesApplyEffect;
        Debug.Log("Pieces stopped");
    }

}
