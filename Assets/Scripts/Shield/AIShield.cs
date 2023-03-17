using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIShield : Shield
{
    Transform damagePopupPrefab;
    Transform mainCamera;
    
    Transform barsTransform;

    CumulativeDamageNumbers popup;

    float takenDamageSum;

    [SerializeField] IShieldBar sb;

    void OnEnable()
    {
        damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");
        mainCamera = Camera.main.transform;

        popup = GetComponentInChildren<CumulativeDamageNumbers>(true);

        materialPropertyBlock = new();

        eh = GetComponent<TankEntity>();
        eh.ShieldScript = this;

        baseSP = eh.hullCard.baseSP;
        maxSP = baseSP;
        currentSP = maxSP;

        rechargeRate = eh.hullCard.shieldRechargeRate;
        rechargeDelay = eh.hullCard.shieldRechargeDelay;

        //Shield bar
        sb = GetComponentInChildren<IShieldBar>(true);
        sb.StartBar();
        //sb.ConnectShield(this);
        sb.ChangeMaxSP(maxSP);
        sb.UpdateBar(currentSP);

        barsTransform = transform.Find("floatingBars");

        sounds = transform.Find("Sounds");
        takingHitSound = sounds.Find("TakingHitSoundShield").GetComponent<AudioSource>();
        takingEMPSound = sounds.Find("TakingEMPSoundShield").GetComponent<AudioSource>();
        shieldBrokenSound = sounds.Find("ShieldDestroyed").GetComponent<AudioSource>();
        shieldRechargingSound = sounds.Find("ShieldRegenSound").GetComponent<AudioSource>();
    }

    private void Start()
    {
        //meshRenderers = eh.meshRenderers;
        EnableShieldShader();
    }
    
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

            sb.UpdateBar(currentSP);
        }
    }

  

    public override void TakingDMG(Damage dmgInstance)
    {
        takingHitSound.Play();
        StopShieldRecharge();

        if (takenDamageSum == 0)
        {
            StartCoroutine(AccumulateDMG());

        }
        takenDamageSum += Mathf.Clamp(dmgInstance.damage, 0, currentSP);

        PopupAdd(takenDamageSum);

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
        takingEMPSound.Play();
        StopShieldRecharge();

        if (takenDamageSum == 0)
        {
            StartCoroutine(AccumulateDMG());
        }

        PopupAdd(dmgInstance.damage);

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

    public override void ChangeCurrentSP(float change)
    {
        base.ChangeCurrentSP(change);
        sb.UpdateBar(currentSP);

    }

    void PopupAdd(float damage)
    {

        if (popup.gameObject.activeSelf == false)
        {
            popup.gameObject.SetActive(true);
        }
        popup.AddValue(damage);
    }

    IEnumerator AccumulateDMG()
    {
        yield return new WaitForEndOfFrame();

        DamageNumbersPopup.CreateStatic(damagePopupPrefab, barsTransform, takenDamageSum, Color.blue);
        takenDamageSum = 0;

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
        materialPropertyBlock.SetFloat("_isShieldUp", 1.0f);
        foreach (MeshRenderer item in eh.meshRenderers)
        {
            item.SetPropertyBlock(materialPropertyBlock);
        }
    }

    public override void DisableShieldShader()
    {
        materialPropertyBlock.SetFloat("_isShieldUp", 0.0f);
        foreach (MeshRenderer item in eh.meshRenderers)
        {
            item.SetPropertyBlock(materialPropertyBlock);
        }
    }

}
