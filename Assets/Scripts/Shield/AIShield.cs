using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIShield : Shield
{
    Transform damagePopupPrefab;
    Transform mainCamera;

    Slider enemyHealthBar;
    Transform healthBarTransform;

    DamageNumbersPopup popup;

    float takenDamageSum;
    void OnEnable()
    {
        damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");
        mainCamera = Camera.main.transform;

        //Health bar
        enemyHealthBar = GetComponentInChildren<Slider>(true); 
        healthBarTransform = enemyHealthBar.transform;

        materialPropertyBlock = new();

        eh = GetComponent<EntityHandler>();
        eh.ShieldScript = this;

        baseSP = eh.hullCard.baseSP;
        maxSP = baseSP;
        currentSP = maxSP;

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
        }
    }

  

    public override void TakingDMG(Damage dmgInstance)
    {
        takingHitSound.Play();
        StopShieldRecharge();

        //if (takenDamageSum == 0)
        //{
        //    StartCoroutine(AccumulateDMG());

        //}
        //takenDamageSum += Mathf.Clamp(damage, 0, currentSP);

        PopupCreate(dmgInstance.damage);

        currentSP -= dmgInstance.damage;  

        if (currentSP <= 0)
        {
            shieldBrokenSound.Play();
            DisableShieldShader();

            eh.HealthScript.OverDamage(new Damage(0 - currentSP, dmgInstance.source));
            //eh.health.popup = popup;
            
            currentSP = 0;
        }


    }

    public override void TakingEMP(Damage dmgInstance)
    {
        takingEMPSound.Play();
        StopShieldRecharge();

        PopupCreate(dmgInstance.damage);

        currentSP -= dmgInstance.damage;

        if (currentSP <= 0)
        {
            shieldBrokenSound.Play();
            DisableShieldShader();

            eh.LoseEMPTenacity(new Damage(0 - currentSP, dmgInstance.source));

            currentSP = 0;
        }

        

    }

    void PopupCreate(float damage)
    {
        
        damage = Mathf.Clamp(damage, 0, currentSP);
        if (popup == null)
        {
            //print("null");
            popup = DamageNumbersPopup.CreateStatic(damagePopupPrefab, healthBarTransform.position + transform.up + mainCamera.right, damage, Color.blue);
            popup.transform.SetParent(gameObject.transform);
        }
        else
        {
            //print("NOT null");
            popup.ChangeText(damage);
        }
    }

    //IEnumerator AccumulateDMG()
    //{
    //    yield return new WaitForEndOfFrame();

    //    DamageNumbersPopup.Create(damagePopupPrefab, transform.position + Vector3.up * 2, mainCamera.transform.right, takenDamageSum, Color.blue);
    //    takenDamageSum = 0;

    //}

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
