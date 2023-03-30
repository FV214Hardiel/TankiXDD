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

    public bool IsStatusAffectable { get { return isAffectable; } }

    bool isAffectable;

    private void Start()
    {
        entity = GetComponentInParent<IEntity>();
        if (entity.EffH == null) 
        {
            Destroy(gameObject);
            return;
        }
        isAffectable = entity.EffH.isStatusAffectable;
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
