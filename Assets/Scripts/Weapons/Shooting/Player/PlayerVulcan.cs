using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVulcan : PlayerShooting
{
    

    [SerializeField] byte stacks;
    public float stackDuration;
    float stackTimer;
    float modifiedDelay;   

    Vector3 shotVector;

    AudioSource chargeSound;

    public GameObject prefabOfShot;
    public ParticleSystem shotEffect;

    void Start()
    {
        source = GetComponentInParent<IEntity>();
        muzzle = transform.Find("muzzle");       

        inputActions = new();
        if (!GameHandler.instance.GameIsPaused) inputActions.PlayerTankControl.Enable();

        //shotSound = GetComponent<AudioSource>();
        chargeSound = transform.Find("ChargesSound").GetComponent<AudioSource>();

        remainingDelay = 0;

        friendlyMask = source.FriendlyMasks;

        disperseAngles = new();
        for (int _ = 0; _ < 50; _++)
        {
            disperseAngles.Add((ushort)Random.Range(1, 357));
        }

        disperseLengths = new();
        for (int _ = 0; _ < 50; _++)
        {
            disperseLengths.Add(Random.Range(0f, 1f));
        }

        index = 0;

        stacks = 0;
        stackTimer = 0;
        
    }
    
    void Update()
    {
        if (GameHandler.instance.GameIsPaused) return; //Checking pause

        chargeSound.pitch = stacks > 0 ? (0.4f + stacks * 0.03f) : 0;

        if (remainingDelay > 0) //Decreasing delay timer  
        {
            remainingDelay -= Time.deltaTime;
            return;
        }
        //Stacks are decreasing with time
        stackTimer -= Time.deltaTime;
        if (stackTimer <= 0 && stacks > 0)
        {
            stacks--;
            stackTimer = stackDuration;
        }
       

        inputValue = inputActions.PlayerTankControl.Fire.ReadValue<float>();

        if (inputValue > 0) //Shot
        {
            shotVector = DisperseVector(muzzle.forward, angle);
            Shot(shotVector);
        }
        
    }

    

    void Shot(Vector3 shotVector)
    {        
        
        shotEffect.Play();
        
        if (Physics.Raycast(muzzle.position, shotVector, out RaycastHit hit, weapRange, ~friendlyMask))
        {
            IDamagable damagable = hit.collider.GetComponentInParent<IDamagable>();
            if (damagable != null)
            {
                
                if (!damagable.IsDead)
                {
                    
                    damagable.DealDamage(damage, source);
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
}
