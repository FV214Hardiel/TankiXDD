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

    protected EntityHandler eh;

    protected Transform sounds;

    protected AudioSource takingHitSound;
    protected AudioSource takingEMPSound;
    protected AudioSource shieldBrokenSound;
    protected AudioSource shieldRechargingSound;

    protected List<MaterialPropertyBlock> materialPropertyBlocks;
    protected List<MeshRenderer> meshRenderers;
    protected MaterialPropertyBlock materialPropertyBlock;

    public virtual void StartShieldRecharge()
    {

    }
   

    public virtual void StopShieldRecharge()
    {

    }

    public virtual void TakingDMG(Damage dmgInstance)
    {
        takingHitSound.Play();
        StopShieldRecharge();


    }

    public virtual void TakingEMP(Damage dmgInstance)
    {

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
    }

    public virtual void EnableShieldShader() { }

    public virtual void DisableShieldShader() { }


}
