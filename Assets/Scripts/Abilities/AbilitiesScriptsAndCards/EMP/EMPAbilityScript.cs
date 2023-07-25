using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EMPAbilityScript : AbilityBase
{

    enum AbilityState { ready, cooldown }
    AbilityState state = AbilityState.ready;

    IEntity source;

    new void Start()
    {
        base.Start();

        source = GetComponent<IEntity>();
    }
        
    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetButtonDown(abilityKey))
                {

                    state = AbilityState.cooldown;
                    UseAbility();
                    remainingCooldown = abilityCooldown;

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
        print("EMP Radius " + abilityArea);
    }

}
