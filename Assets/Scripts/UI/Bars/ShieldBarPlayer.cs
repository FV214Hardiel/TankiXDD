using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarPlayer : MonoBehaviour, IShieldBar
{
    public Shield shield;

    public RectTransform Bar;
    float barBaseWidth;

    TMPro.TextMeshProUGUI TextText;

    public float maxShield;

    float perc;


    void Awake()
    {
       // print("awake");
        

    }

    public void StartBar()
    {
        barBaseWidth = Bar.rect.width;
        //print("startbar");
    }

    public void ChangeMaxSP(float maxSP)
    {
        maxShield = maxSP;
    }

    public void UpdateBar(float newSP) 
    {
       // print("update");
        
        perc = barBaseWidth * (newSP / maxShield);

       
        Bar.sizeDelta = new Vector2(perc, 40);
        
        newSP = Mathf.Floor(newSP);

        TextText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        TextText.text = newSP.ToString();
    }

    public void ConnectShield(Shield shield)
    {
        this.shield = shield;
    }
}
