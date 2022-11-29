using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public float damage;
    public IEntity source;
    public float armorPenetration;

    public Damage(float damage, IEntity source)
    {
        this.damage = damage;
        this.source = source;
        armorPenetration = 0;
    }

    public Damage (IEntity source)
    {
        damage = 0;
        this.source = source;
        armorPenetration = 0;
    }
}
