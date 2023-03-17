using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    GameObject Gameobject { get; }

    Health HealthScript { get; set; }
    Shield ShieldScript { get; set; }

    bool IsDead { get; }

    EffectsHandler EffH { get; set; }

    LayerMask EnemiesMasks { get; set; }
    LayerMask FriendlyMasks { get; set; }


    ReceivingDamageEffects PropertyReceivingDamageEffects { get; set; }

    void DealDamage(Damage dmgInstance);
    void DealEMP(Damage dmgInstance);

    void DealAOE(Damage dmgInstance);

    event Action EntityStunned;
    event Action EntityAwaken;

    bool isPlayer { get; }


}
