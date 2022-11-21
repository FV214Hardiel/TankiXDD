using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : Effect
{

    public float innerBaseCD;
    float innerCD;
    Health heatlh;

    LevelStatisticsManager statsInstance;
    public Regeneration(float duration, float power)
    {
        effectName = "Regeneration";

        effectDuration = duration;
        effectPower = power;
        remainingDuration = duration;       
        

        innerBaseCD = 0.5f;
        effectID = "id_regeneration";

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
        //Finding HP script
        heatlh = affectedObject.HealthScript;

              

        if (affectedObject == Player.PlayerEntity)
        {
            isPlayer = true;
            statsInstance = LevelStatisticsManager.instance;
        }

    }
   
    
    public void UseEffect()
    {        
        float heal = heatlh.Heal(effectPower * effectStacks, affectedObject);

        if (isPlayer) statsInstance.AddValue(effectID, heal);


    }

    public override void EndEffect()
    {

    }
}
