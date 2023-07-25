using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverloadScript : AbilityBase
{
    enum AbilityState { ready, cooldown }
    AbilityState state = AbilityState.ready;

    TankEntity source;


   
    new void Start()
    {
        //Basic setup
        base.Start();

        source = GetComponent<TankEntity>();

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
        print("test overload");
        source.effh.AddEffect(new Overload(abilityDuration));
    }
}
