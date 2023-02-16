using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeShieldScript : AbilityBase
{
    enum AbilityState { ready, active, cooldown }
    AbilityState state = AbilityState.ready;

    float remainingDuration;
    IEntity source;

    GameObject deployedShield;

    

    void Start()
    {
        GetStats();

        SetIcon();

        source = GetComponent<IEntity>();

        

        
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
        //print("Dome shield " + abilityDuration);

        deployedShield = Instantiate(abilityPrefab, transform, false);
        deployedShield.transform.localScale = Vector3.one * 10;
        deployedShield.layer = source.Gameobject.layer;

    }

    void DisableEffect()
    {
        Destroy(deployedShield);
        state = AbilityState.cooldown;
    }
}
