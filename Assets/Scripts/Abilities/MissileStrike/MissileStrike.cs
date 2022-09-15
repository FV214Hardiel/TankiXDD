using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissileStrike : AbilityBase
{
    

    enum AbilityState { ready, aiming, cooldown}
    AbilityState state = AbilityState.ready;

    public KeyCode abilityCancel;

    public GameObject groundArea;
    public GameObject Missile;
    Vector3 target;
    public Transform pointer;

    float remCD;    
    public float spawnHeight = 100;
    
    void Start()
    {
        GetStats();

        SetIcon();

        //Debug.Log(abilitySlot);
        Missile = Resources.Load<GameObject>("Weaponry/Abilities/missile");        

        groundArea = Instantiate(Resources.Load<GameObject>("Weaponry/Abilities/AbilityAreaVisual"), transform);
        groundArea.transform.localScale *= abilityArea;
        groundArea.SetActive(false);

        pointer = GameObject.Find("Poinet").transform;


        
    }

    
    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetButtonDown(abilityKey))
                {
                    
                    state = AbilityState.aiming;
                    groundArea.SetActive(true);
                    target = Aim(abilityRange);
                    groundArea.transform.position = target;

                }
                break;
            case AbilityState.aiming:
                target = Aim(abilityRange);
                groundArea.transform.position = target;
                if (Input.GetButtonDown(abilityKey))
                {
                    state = AbilityState.cooldown;
                    Launch();
                    remCD = abilityCooldown;
                    groundArea.SetActive(false);
                }
                if (Input.GetKeyDown(abilityCancel))
                {
                    state = AbilityState.ready;
                    groundArea.SetActive(false);
                }

                break;
            case AbilityState.cooldown:
                if (remCD > 0)
                {
                    remCD -= Time.deltaTime;
                    abilityDisplay.localScale = new Vector3(1, remCD / abilityCooldown, 1);
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }
    public float GetCD()
    {
        return remCD;
    }
    public void Launch()
    {
       GameObject newMissile = Instantiate(Missile, target + Vector3.up  * spawnHeight, transform.rotation);
       newMissile.transform.LookAt(target, Vector3.up);
       newMissile.GetComponent<MissileCollideScript>().MissileStats(abilityDamage, abilityArea, target, Player.PlayerHull);

    }
    
    Vector3 Aim(float range)
    {
        Vector3 direction = pointer.transform.position - transform.position;
        Ray aimRay = new Ray(transform.position, direction);
       
        if (Physics.Raycast(aimRay, out RaycastHit hit2, maxDistance: range))
            {
            return hit2.point;
        }
        else
        {
            direction.Normalize();
            return Vector3.ProjectOnPlane(transform.position + direction * range, Vector3.up);
        }

    }
}
