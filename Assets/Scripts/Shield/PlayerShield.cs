using System;
using UnityEngine;

public class PlayerShield : Shield
{

    [SerializeField] ShieldBarPlayer sb;
    // Start is called before the first frame update
    void OnEnable()
    {
        eh = GetComponent<EntityHandler>();
        eh.ShieldScript = this;
                
        materialPropertyBlock = new();        

        baseSP = eh.hullCard.baseSP;
        maxSP = baseSP;
        currentSP = maxSP;        

        //Handling UI ShieldBar
        sb = GameObject.Find("ShieldBarUI").GetComponent<ShieldBarPlayer>();
        sb.enabled = true;

        sb.maxShield = maxSP;
        sb.UpdateBar(currentSP);

        rechargeRate = eh.hullCard.shieldRechargeRate;
        rechargeDelay = eh.hullCard.shieldRechargeDelay;

        sounds = transform.Find("Sounds");
        takingHitSound = sounds.Find("TakingHitSoundShield").GetComponent<AudioSource>();
        takingEMPSound = sounds.Find("TakingEMPSoundShield").GetComponent<AudioSource>();
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

    public override void TakingDMG(Damage dmgInstance)
    {
        takingHitSound.Play();
        StopShieldRecharge();
        

        currentSP -= dmgInstance.damage;
        if (currentSP <= 0)
        {
            shieldBrokenSound.Play();
            DisableShieldShader();
            
            eh.HealthScript.OverDamage(new Damage(0 - currentSP, dmgInstance.source));

            currentSP = 0;
        }
        
        sb.UpdateBar(currentSP);

    }

    public override void TakingEMP(Damage dmgInstance)
    {
        takingHitSound.Play();
        StopShieldRecharge();

        currentSP -= dmgInstance.damage;
        if (currentSP <= 0)
        {
            shieldBrokenSound.Play();
            DisableShieldShader();

            eh.LoseEMPTenacity(new Damage(0 - currentSP, dmgInstance.source));

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
