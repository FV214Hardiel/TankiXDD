using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingShieldBar : MonoBehaviour, IShieldBar
{

    public Shield shield;

    public float maxShield;

    Slider barSlider;


    public void StartBar()
    {
        barSlider= GetComponent<Slider>();
        
    }

    public void ChangeMaxSP(float maxSP)
    {
        maxShield = maxSP;
    }

    public void UpdateBar(float newSP)
    {
        barSlider.value= newSP/maxShield;

        
    }

    public void ConnectShield(Shield shield)
    {
        this.shield = shield;
    }
}
