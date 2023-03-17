using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    [SerializeField] protected float baseSP;
    public float maxSP;
    public float currentSP;

    [SerializeField] protected float rechargeDelay;
    [SerializeField] protected float rechargeRate;
    protected bool isRecharging;

    protected TankEntity eh;

    protected Transform sounds;

    protected AudioSource takingHitSound;
    protected AudioSource takingEMPSound;
    protected AudioSource shieldBrokenSound;
    protected AudioSource shieldRechargingSound;

    protected List<MaterialPropertyBlock> materialPropertyBlocks;
   // protected List<MeshRenderer> meshRenderers;
    protected MaterialPropertyBlock materialPropertyBlock;

    bool isPlayer;

    Transform damagePopupPrefab;
    //Transform mainCamera;

    Transform barsTransform;

    CumulativeDamageNumbers popup;

    float takenDamageSum;

    [SerializeField] IShieldBar sb;

    private void OnEnable()
    {
        materialPropertyBlock = new();

        eh = GetComponent<TankEntity>();
        eh.ShieldScript = this;

        isPlayer = eh.isPlayer;

        baseSP = 1;
        maxSP = baseSP;
        currentSP = maxSP;

        //rechargeRate = eh.hullCard.shieldRechargeRate;
        //rechargeDelay = eh.hullCard.shieldRechargeDelay;

        rechargeRate = 1;
        rechargeDelay = 1;

        if (isPlayer)
        {
            sb = GameObject.Find("ShieldBarUI").GetComponent<ShieldBarPlayer>();          
            
        }
        else
        {
            //Shield bar
            sb = GetComponentInChildren<FloatingShieldBar>(true);           

            barsTransform = transform.Find("floatingBars");

            damagePopupPrefab = Resources.Load<Transform>("DamageNumbersPopup");

            popup = GetComponentInChildren<CumulativeDamageNumbers>(true);
        }
        sb.StartBar();
        sb.ChangeMaxSP(maxSP);
        sb.UpdateBar(currentSP);

        sounds = transform.Find("Sounds");
        takingHitSound = sounds.Find("TakingHitSoundShield").GetComponent<AudioSource>();
        takingEMPSound = sounds.Find("TakingEMPSoundShield").GetComponent<AudioSource>();
        shieldBrokenSound = sounds.Find("ShieldDestroyed").GetComponent<AudioSource>();
        shieldRechargingSound = sounds.Find("ShieldRegenSound").GetComponent<AudioSource>();

    }

    public void UpdateShieldBase(float baseSP, float rate, float delay)
    {
        this.baseSP = baseSP;
        maxSP = baseSP;
        currentSP = baseSP;
        rechargeRate = rate;
        rechargeDelay = delay;

    }

    private void Start()
    {        
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

            if (isPlayer)
            shieldRechargingSound.pitch = 0.6f + (currentSP / maxSP) * 2;

            sb.UpdateBar(currentSP);
        }
    }

    public virtual void StartShieldRecharge()
    {
        isRecharging = true;

        if (isPlayer)
            shieldRechargingSound.Play();

        EnableShieldShader();
    }
   

    public virtual void StopShieldRecharge()
    {
        isRecharging = false;

        if (isPlayer)
            shieldRechargingSound.Stop();
        
    }

    public virtual void TakingDMG(Damage dmgInstance)
    {
        takingHitSound.Play();
        StopShieldRecharge();

        if (!isPlayer)
        {
            if (takenDamageSum == 0)
            {
                StartCoroutine(AccumulateDMG());

            }
            takenDamageSum += Mathf.Clamp(dmgInstance.damage, 0, currentSP);

            PopupAdd(takenDamageSum);
        }
        

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

    public virtual void TakingEMP(Damage dmgInstance)
    {
        takingEMPSound.Play();
        StopShieldRecharge();

        if (!isPlayer)
        {
            if (takenDamageSum == 0)
            {
                StartCoroutine(AccumulateDMG());
            }

            PopupAdd(dmgInstance.damage);
        }       

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

    public virtual void ChangeCurrentSP(float change)
    {
        if (change < 0 && currentSP == 0)
        {
            return;
        }

        currentSP += change;

        if (currentSP <= 0)
        {
            shieldBrokenSound.Play();
            DisableShieldShader();

            currentSP= 0;
        }
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
        if (takenDamageSum < 1) takenDamageSum = 1;
        DamageNumbersPopup.CreateStatic(damagePopupPrefab, barsTransform, takenDamageSum, Color.blue);
        takenDamageSum = 0;

    }

    public virtual void EnableShieldShader() 
    {
        materialPropertyBlock.SetFloat("_isShieldUp", 1.0f);
        foreach (MeshRenderer item in eh.meshRenderers)
        {
            item.SetPropertyBlock(materialPropertyBlock);
        }
    }

    public virtual void DisableShieldShader() 
    {
        materialPropertyBlock.SetFloat("_isShieldUp", 0.0f);
        foreach (MeshRenderer item in eh.meshRenderers)
        {
            item.SetPropertyBlock(materialPropertyBlock);
        }
    }


}
