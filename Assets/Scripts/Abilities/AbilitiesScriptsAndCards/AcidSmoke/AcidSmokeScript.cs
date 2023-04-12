using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSmokeScript : AbilityBase
{

    enum AbilityState { ready, active, cooldown }
    AbilityState state = AbilityState.ready;

    float remainingDuration;
    IEntity source;

    GameObject deployedCloud;


    new void Start()
    {
        base.Start();

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

        deployedCloud = Instantiate(abilityPrefab, transform.position, Quaternion.identity);
        //deployedCloud.transform.localScale = Vector3.one * 10;
        deployedCloud.GetComponent<AcidSmokeEntityScript>().Init(abilityDamage, abilityArea, source);
        //deployedCloud.layer = source.Gameobject.layer;

    }

    void DisableEffect()
    {
        Destroy(deployedCloud);
        state = AbilityState.cooldown;
    }
}
