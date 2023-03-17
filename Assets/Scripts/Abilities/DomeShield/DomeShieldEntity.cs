using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomeShieldEntity : MonoBehaviour, IDamagable
{

    public IEntity Entity { get { return baseEntity; } }

    IEntity baseEntity;

    public GameObject Gameobject { get {return gameObject; } }
    public bool IsDead { get { return isActive; }}

    public bool IsStatusAffectable { get { return isAffectable; } }

    bool isAffectable = false;

    bool isActive;

    void Start()
    {
        baseEntity = GetComponentInParent<TankEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(Damage dmgInstance)
    {
        print("shieldDamaged");
    }

    public void DealEMP(Damage dmgInstance)
    {
        print("shieldEMPed");
    }

    public void DealAOE(Damage dmgInstance)
    {
        print("shieldAOEd");
    }

    void DisableShield()
    {
        Destroy(this);
    }
}
