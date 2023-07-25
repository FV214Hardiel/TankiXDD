using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldOverloadScript : AbilityBase
{
    enum AbilityState { ready, active, cooldown }
    AbilityState state = AbilityState.ready;

    float remainingDuration;
    IEntity source;

    Weapon shotScript;

    new void Start()
    {
        base.Start();

        source = GetComponent<IEntity>();

        shotScript = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Update()
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
        print("ShieldOverload " + abilityDuration);

        shotScript.outputDamageModifiers += Modifier;
      //  source.ShieldScript.maxSP
    }

    void DisableEffect()
    {
        shotScript.outputDamageModifiers -= Modifier;
    }

    float Modifier(float dmg)
    {
        float newdmg = dmg + source.ShieldScript.currentSP * 0.05f;
        source.ShieldScript.ChangeCurrentSP(-10);
        return newdmg;
    }
}
