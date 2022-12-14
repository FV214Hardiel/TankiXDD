using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour, IDamagable
{
    public float multiplier;

    IEntity entity;

    public GameObject Gameobject { get { return gameObject; } }

    public IEntity Entity { get { return entity; } }

    public bool IsDead { get { return entity.IsDead; } set { return; } }

    private void Start()
    {
        entity = GetComponentInParent<IEntity>();
        gameObject.layer = entity.Gameobject.layer;
        //print(Gameobject.name);
    }

    public void DealEMP(Damage dmgInstance)
    {
        entity.DealEMP(dmgInstance);
    }

    public void DealDamage(Damage dmgInstance)
    {
        //print("damage zone deal damage");
        dmgInstance.damage *= multiplier;
        entity.DealDamage(dmgInstance);
        
    }

    public void DealAOE(Damage dmgInstance)
    {
        entity.DealDamage(dmgInstance);
    }


}
