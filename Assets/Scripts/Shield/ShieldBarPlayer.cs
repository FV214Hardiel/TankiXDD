using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarPlayer : MonoBehaviour
{
    public PlayerShield shield;

    public RectTransform Bar;
    float barBaseWidth;

    TMPro.TextMeshProUGUI TextText;

    public float maxShield;    
    
    void Awake()
    {
        barBaseWidth = Bar.rect.width;
    }

    

    public void UpdateBar(float newSP) 
    {
        float perc = barBaseWidth * (newSP / maxShield);
        Bar.sizeDelta = new Vector2(perc, 40);
        
        newSP = Mathf.Floor(newSP);

        TextText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        TextText.text = newSP.ToString();
    }
}
