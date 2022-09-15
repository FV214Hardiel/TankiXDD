using System;
using UnityEngine;

public class PlayerShield : Shield
{

    [SerializeField] ShieldBarPlayer sb;
    // Start is called before the first frame update
    void OnEnable()
    {
        eh = GetComponent<EntityHandler>();
        eh.shield = this;

        //materialPropertyBlocks = eh.materialPropertyBlocks;
        materialPropertyBlock = new();

        

        baseSP = eh.tankCard.baseSP;
        maxSP = baseSP;
        currentSP = maxSP;

        //Debug.Log("RAZ");

        //Handling UI ShieldBar
        sb = GameObject.Find("ShieldBarUI").GetComponent<ShieldBarPlayer>();
        sb.enabled = true;
        sb.shield = this;

        rechargeRate = eh.tankCard.shieldRechargeRate;
        rechargeDelay = eh.tankCard.shieldRechargeDelay;

        sounds = transform.Find("Sounds");
        takingHitSound = sounds.Find("TakingHitSoundShield").GetComponent<AudioSource>();
        shieldBrokenSound = sounds.Find("ShieldDestroyed").GetComponent<AudioSource>();
        shieldRechargingSound = sounds.Find("ShieldRegenSound").GetComponent<AudioSource>();
    }


    private void Start()
    {
        meshRenderers = eh.meshRenderers;
        EnableShieldShader();
    }
    // Update is called once per frame
    void Update()
    {
        if (eh.outOfDamage > rechargeDelay && !isRecharging && currentSP < maxSP)
        {
            StartShieldRecharge();
        }

        if (isRecharging)
        {
            currentSP += rechargeRate * Time.deltaTime; 
                        
            if (currentSP >= maxSP)
            {
                currentSP = maxSP;
                StopShieldRecharge();
            }
                        

            shieldRechargingSound.pitch = 0.6f + (currentSP / maxSP) * 2;

            sb.UpdateBar(currentSP);
        }
    }

    public override void TakingDMG(float damage, GameObject source)
    {
        takingHitSound.Play();
        StopShieldRecharge();
        eh.outOfDamage = 0;

        currentSP -= damage;
        if (currentSP <= 0)
        {
            shieldBrokenSound.Play();
            DisableShieldShader();
            
            eh.health.OverDamage(0 - currentSP, source);

            currentSP = 0;
        }
        
        sb.UpdateBar(currentSP);

    }

    

    public override void StartShieldRecharge()
    {
        isRecharging = true;
        shieldRechargingSound.Play();

        EnableShieldShader();

        //Debug.Log("StartRecharge");
    }
    public override void StopShieldRecharge()
    {
        isRecharging = false;
        shieldRechargingSound.Stop();        
        //Debug.Log("Stop Recharge");
    }

    public override void EnableShieldShader() 
    {
        //materialPropertyBlock.SetFloat("_isShieldUp", 1.0f);
        //meshRenderer.SetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_isShieldUp", 1.0f);
        foreach (MeshRenderer item in meshRenderers)
        {
            
            item.SetPropertyBlock(materialPropertyBlock);
        }
    }

    public override void DisableShieldShader() 
    {
        //materialPropertyBlock.SetFloat("_isShieldUp", 0.0f);
        //meshRenderer.SetPropertyBlock(materialPropertyBlock);
        materialPropertyBlock.SetFloat("_isShieldUp", 0.0f);
        foreach (MeshRenderer item in meshRenderers)
        {

            item.SetPropertyBlock(materialPropertyBlock);
        }
    }

}
