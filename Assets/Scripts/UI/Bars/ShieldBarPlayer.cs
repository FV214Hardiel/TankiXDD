using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBarPlayer : MonoBehaviour, IShieldBar
{
    public Shield shield;

    public Slider bar;

    TMPro.TextMeshProUGUI TextText;

    public float maxShield;

    public void StartBar()
    {
        TextText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public void ChangeMaxSP(float maxSP)
    {
        maxShield = maxSP;
    }

    public void UpdateBar(float newSP) 
    {        
        bar.value = newSP / maxShield;

        newSP = Mathf.Floor(newSP);
        
        TextText.text = newSP.ToString();
    }

    public void ConnectShield(Shield shield)
    {
        this.shield = shield;
    }
}
