using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedboostScript : AbilityBase
{
    enum AbilityState { ready, active, cooldown }
    AbilityState state = AbilityState.ready;


    float remainingDuration;
    IEntity source;

    Move moveScript;
    void Start()
    {
        GetStats();

        SetIcon();

        source = GetComponent<IEntity>();

        moveScript = GetComponent<Move>();

        //print(GetComponent<Move>());
        
    }

    private void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetButtonDown(abilityKey))
                {

                    state = AbilityState.active;
                    UseAbility();
                    remainingCooldown = abilityCooldown;
                    remainingDuration = abilityDuration;

                }
                break;
            case AbilityState.active:
                abilityDisplay.localScale = Vector3.one;
                if (remainingDuration > 0)
                {

                    remainingDuration -= Time.deltaTime;
                }
                else
                {
                    DisableEffect();
                    state = AbilityState.cooldown;


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
        print("Nitro " + abilityDuration);
        moveScript.msMultipliers.Add(abilityPower);
        moveScript.RecalculateSpeed();
    }

    void DisableEffect()
    {
        moveScript.msMultipliers.Remove(abilityPower);
        moveScript.RecalculateSpeed();
    }


}
