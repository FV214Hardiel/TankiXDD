using LlockhamIndustries.Decals;
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
        //affectedObject.Gameobject.GetComponentInChildren<Shooting>().hitDelegate += Test;

    }   
   

    public override void EndEffect()
    {
        affectedObject.Gameobject.GetComponentInChildren<Shooting>().DisableOverload();
        //affectedObject.Gameobject.GetComponentInChildren<Shooting>().hitDelegate -= Test;
    }

    public void Test(IDamagable hit, Damage dmg)
    {
        //Debug.Log(hit.Entity.Gameobject);
        //hit.DealEMP(new Damage(dmg.damage * 20, dmg.source));
    }
}
