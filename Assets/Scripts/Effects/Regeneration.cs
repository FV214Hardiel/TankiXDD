using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Effect
{

    public float innerBaseCD;
    float innerCD;
    Health heatlh;
    public Regeneration(float duration, float power)
    {
        effectDuration = duration;
        effectPower = power;
        remainingDuration = duration;

        
        heatlh = affectedObject.health;

        innerBaseCD = 0.5f;
        effectID = 1;

        isEffectStackable = true;
        isTimeStackable = false;

        effectStacks = 1;

        
        
    }

    public override float Tick(float deltatime)
    {
        
           
        innerCD -= deltatime;
        if (innerCD <= 0)
        {
            UseEffect();
            innerCD = innerBaseCD;
        }

        remainingDuration -= deltatime;
        if (remainingDuration <= 0)
        {
            remainingDuration = 0;

        }


        return remainingDuration;
    }

    public override void InitEffect()
    {

    }
        

    //
    public void UseEffect()
    {
               
        Debug.Log("HEAL: " + effectPower * effectStacks);
        //Debug.Log(affectedObject.GetComponent<Shield>());
    }

    public override void EndEffect()
    {

    }
}
