using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadScript : AbilityBase
{
    enum AbilityState { ready, cooldown }
    AbilityState state = AbilityState.ready;

    EntityHandler source;


   
    void Start()
    {
        //Basic setup
        GetStats();

        SetIcon();

        source = GetComponent<EntityHandler>();

        //Specific setup
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetButtonDown(abilityKey))
                {                    
                    remainingCooldown = abilityCooldown;
                    state = AbilityState.cooldown;
                    UseAbility();
                    
                }
                break;
           
            case AbilityState.cooldown:
                if (remainingCooldown > 0)
                {
                    remainingCooldown -= Time.deltaTime;
                    abilityDisplay.localScale = new Vector3(1, remainingCooldown / abilityCooldown, 1);
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    void UseAbility()
    {
        source.effh.AddEffect(new Overload(abilityDuration));
    }
}
