using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHPBar : MonoBehaviour, IHealthBar
{
    //public Health health;

    public float maxHP;

    Slider barSlider;

    public void StartBar()
    {
        barSlider = GetComponent<Slider>();
    }

    public void UpdateBar(float newHP)
    {
        barSlider.value = newHP / maxHP;
    }

    public void ChangeMaxHP(float maxHP)
    {
        this.maxHP = maxHP; 
    }

    //public void ConnectHealth(Health health)
    //{
    //    this.health = health;
    //}
}
