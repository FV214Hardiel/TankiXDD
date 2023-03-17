using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsHandler : MonoBehaviour
{
    public List<Effect> activeEffects;
    public bool isStatusAffectable = true; //Every entity can get buffs/debuffs until opposite is declared

    int i;
   
    void OnEnable()
    {
        activeEffects = new List<Effect>();
    }

    
    void Update()
    {
        
        //For every effect in the list calls their Tick
        //Reverse order is importrant when effect expires and gets deleted
        for (i = activeEffects.Count - 1; i >= 0; i--)
        {            
            if (activeEffects[i].Tick(Time.deltaTime) == 0)
            {               
                activeEffects.Remove(activeEffects[i]);               
            }
        }
    }
    //Shortly: adds effect
    public void AddEffect(Effect effect)
    {
        //Searhing effect in list of active effects on target
        Effect search = activeEffects.Find(x => x.effectID == effect.effectID);

        if (search == null)
        {            
            effect.affectedObject = gameObject.GetComponent<TankEntity>(); 
            activeEffects.Add(effect); //Adding effect
            effect.InitEffect();
        }
        else
        {            
            if (search.isEffectStackable) //Upgrade effect if stackable
            {
                
                search.effectStacks += 1;
                search.remainingDuration = effect.effectDuration;
                search.effectPower = effect.effectPower;
            }
            else 
            {
                if (search.canBeDifferentInstances)
                {
                    effect.affectedObject = gameObject.GetComponent<TankEntity>();
                    activeEffects.Add(effect); //Add effect if additional instances of one effect allowed
                    effect.InitEffect();
                }
                else
                {
                    search.remainingDuration = effect.effectDuration; //Refresh duration if only one effect allowed
                }
                
            }
        }

    }

    //Returns effect if its already on target or NULL if not
    public Effect GetEffect(string effectID)
    {        
        return activeEffects.Find(x => x.effectID == effectID);
    }
}
