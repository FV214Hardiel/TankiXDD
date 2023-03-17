using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour, IHealthBar
{
    public Health health;

    Slider bar;

    TMPro.TextMeshProUGUI TextText;

    float maxHealth;

    public void StartBar()
    {
        TextText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        bar = GetComponent<Slider>();

        bar.value = 0;

    }

    public void ChangeMaxHP(float maxHP)
    {
        maxHealth = maxHP;
    }
        
    public void UpdateBar(float newHP)
    {
        bar.value = newHP / maxHealth;

        newHP = Mathf.Floor(newHP);

        TextText.text = newHP.ToString();
    }

    public void ConnectHealth(Health health)
    {
        this.health = health;
    }

}
