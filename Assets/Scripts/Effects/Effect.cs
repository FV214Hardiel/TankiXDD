using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Effect
//{

//    public enum EffectTypes { Buff, Debuff }
//    public EffectTypes effectType;

//    public GameObject affectedObject;

//    public ushort effectID;

//    public float effectPower;
//    public float effectDuration;
//    public bool isDispellable;

//    public bool isEffectStackable;
//    public int effectStacks;

//    public bool isTimeStackable;


//    public float remainingDuration;
//    public virtual float Tick(float deltatime)
//    {
//        return 0;
//    }

//    public virtual void InitEffect()
//    {

//    }

//}
public abstract class Effect
{
    public string effectName;
    public enum EffectTypes { Buff, Debuff }
    public EffectTypes effectType;

    public IEntity affectedObject;
    public bool isPlayer;

    public string effectID;

    public float effectPower;
    public float effectDuration;
    public bool isDispellable;

    public bool isEffectStackable;
    public int effectStacks;

    public bool isTimeStackable;

    public bool canBeDifferentInstances;

    public float remainingDuration;

    
    public abstract float Tick(float deltatime);

    public abstract void InitEffect();

    public abstract void EndEffect();

}
