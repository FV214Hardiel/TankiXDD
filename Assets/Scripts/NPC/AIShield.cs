using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShield : Shield
{
    Transform damagePopupPrefab;

    // Start is called before the first frame update
    void OnEnable()
    {
        damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");

        materialPropertyBlock = new();

        eh = GetComponent<EntityHandler>();
        eh.shield = this;

        baseSP = eh.hullCard.baseSP;
        maxSP = baseSP;
        currentSP = maxSP;

        rechargeRate = eh.hullCard.shieldRechargeRate;
        rechargeDelay = eh.hullCard.shieldRechargeDelay;

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
        }
    }

    public override void TakingDMG(float damage, GameObject source)
    {
        takingHitSound.Play();
        StopShieldRecharge();
        eh.outOfDamage = 0;

        currentSP -= damage;
        DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, transform.right, damage, Color.blue);

        if (currentSP <= 0)
        {
            shieldBrokenSound.Play();
            DisableShieldShader();

            eh.health.OverDamage(0 - currentSP, source);
            
            currentSP = 0;
        }


    }

    

    public override void StartShieldRecharge()
    {
        isRecharging = true;

        EnableShieldShader();
    }
    public override void StopShieldRecharge()
    {
        isRecharging = false;
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
