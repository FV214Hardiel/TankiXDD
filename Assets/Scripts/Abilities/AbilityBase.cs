using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityBase : MonoBehaviour
{
    //Card
    public AbilityCard card;

    //Stats
    protected string abilityKey;
    protected float abilityArea;   
    protected float abilityRange;
    protected float abilityDamage;
    protected float abilityCooldown;
    protected float abilityPower;
    protected float abilityDuration;

    //UI
    protected Sprite abilityIcon;
    public int abilitySlot;
    protected UIAbilityHandler abilityUI;
    public RectTransform abilityDisplay;
    public RectTransform outline;

    //
    protected float remainingCooldown;
    
    public void GetStats()
    {
        abilityKey = "Ability" + abilitySlot.ToString();       
        abilityPower = card.power;
        abilityDuration = card.duration;
        abilityDamage = card.damage;
        abilityArea = card.radius;
        abilityRange = card.range;
        abilityCooldown = card.cooldown;
        abilityIcon = card.image;
    }

    public void SetIcon()
    {
        abilityUI = FindObjectOfType<UIAbilityHandler>();
        abilityUI.abilitySlots[abilitySlot].GetComponent<Image>().sprite = abilityIcon;
    }

    
}
