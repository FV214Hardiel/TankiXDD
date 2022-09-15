using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityCard", menuName = "Tanks/Abilities")]
public class AbilityCard : ScriptableObject
{
    public string abilityName;
    public Sprite image;

    public string script;

    public KeyCode key;

    public float cooldown;
    public float power;
    public float duration;
    public float damage;
    public float range;
    public float radius;

}
