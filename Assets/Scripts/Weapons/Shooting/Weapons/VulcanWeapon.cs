using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulcanWeapon : Weapon
{
    
    [SerializeField] byte stacks;
    public float stackDuration;
    float baseStackDur;
    [SerializeField] float stackTimer;
    float modifiedDelay;       

    AudioSource chargeSound;

    public GameObject prefabOfShot;
    public ParticleSystem shotEffect;

    new void Start()
    {
        base.Start();
        
        chargeSound = transform.Find("ChargesSound").GetComponent<AudioSource>();
       
        baseStackDur = stackDuration;        

        friendlyMask = source.FriendlyMasks;
        
        InitAnglesAndLengthLists();

        stacks = 0;
        stackTimer = 0;
        
    }
    
    new void Update()
    {
        base.Update();

        chargeSound.pitch = stacks > 0 ? (0.4f + stacks * 0.03f) : 0;

        //Stacks are decreasing with time
        stackTimer -= Time.deltaTime;
        if (stackTimer <= 0 && stacks > 0 && !isOpenFire)
        {
            stacks--;
            stackTimer = stackDuration;
        }      
        
    }

    public override void EnableOverload()
    {
        base.EnableOverload();
        stacks = 17;
        stackDuration = 10;
        stackTimer = 10;

    }

    public override void DisableOverload()
    {
        base.DisableOverload();
        stackDuration = baseStackDur;
        stackTimer = baseStackDur;
    }



    protected override void Shot()
    {
        shotVector = DisperseVector(muzzle.forward, angle);

        shotEffect.Play();
        
        if (Physics.Raycast(muzzle.position, shotVector, out RaycastHit hit, weapRange, ~friendlyMask))
        {
            IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {                
                if (!damagable.IsDead)
                {
                    
                    damagable.DealDamage(new Damage(damage, source));
                }
            }                        

            WeaponTrail.Create(prefabOfShot, muzzle.position, hit.point); //Shot VFX if hit
        }
        else
        {                     
            WeaponTrail.Create(prefabOfShot, muzzle.position, muzzle.position+shotVector*weapRange); //Shot VFX if no hit
        }
        
        //Increasing Attack Speed
        modifiedDelay = (delayBetweenShots * 1.8f) / (stacks + 1 + 0.8f);        
        remainingDelay = modifiedDelay;        

        stacks += 1;
        stacks = (byte)(Mathf.Clamp(stacks, 0, 17));
        stackTimer = stackDuration;
    }

    protected override void OverloadShot()
    {
        shotVector = DisperseVector(muzzle.forward, angle);

        shotEffect.Play();

        if (Physics.Raycast(muzzle.position, shotVector, out RaycastHit hit, weapRange, ~friendlyMask))
        {
            IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {
                if (!damagable.IsDead)
                {
                    damagable.DealDamage(new Damage(damage, source));
                }
            }

            WeaponTrail.Create(prefabOfShot, muzzle.position, hit.point); //Shot VFX if hit
        }
        else
        {
            WeaponTrail.Create(prefabOfShot, muzzle.position, muzzle.position + shotVector * weapRange); //Shot VFX if no hit
        }

        //Increasing Attack Speed
        modifiedDelay = (delayBetweenShots * 1.8f) / (stacks + 1 + 0.8f);
        remainingDelay = modifiedDelay;

        stacks = 17;
        
        stackTimer = stackDuration;
    }
}
