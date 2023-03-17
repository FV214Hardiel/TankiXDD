using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    IEntity Entity { get; }
    GameObject Gameobject { get; }

    bool IsDead { get; }
    public bool IsStatusAffectable { get; }

    void DealDamage(Damage dmgInstance);
    void DealEMP(Damage dmgInstance);

    void DealAOE(Damage dmgInstance);

}
