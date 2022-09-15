using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBarPlayer : MonoBehaviour
{
    public PlayerShield shield;

    public RectTransform Bar;
    float barBaseWidth;

    TMPro.TextMeshProUGUI TextText;

    float maxShield;
    
    // Start is called before the first frame update
    void Start()
    {
        barBaseWidth = Bar.rect.width;

        maxShield = shield.maxSP;

        TextText = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        TextText.text = shield.currentSP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBar(float newSP) 
    {
        float perc = barBaseWidth * (newSP / maxShield);
        Bar.sizeDelta = new Vector2(perc, 40);
        
        newSP = Mathf.Floor(newSP);
        TextText.text = newSP.ToString();
    }
}
