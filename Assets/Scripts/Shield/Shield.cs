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

    }

    public virtual void TakingEMP(Damage dmgInstance)
    {

    }

    public virtual void EnableShieldShader() { }

    public virtual void DisableShieldShader() { }


}
