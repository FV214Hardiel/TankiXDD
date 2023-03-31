using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUIDisplay : MonoBehaviour
{
    TextMeshProUGUI currencyDisplay;
   
    void Start()
    {
        currencyDisplay = GetComponent<TextMeshProUGUI>();
        GameInfoSaver.instance.CurrencyProp.CurrencyChanged += UpdateText;
        UpdateText();
    }

    private void OnDisable()
    {
        GameInfoSaver.instance.CurrencyProp.CurrencyChanged -= UpdateText;
    }

    void UpdateText()
    {
        currencyDisplay.text = GameInfoSaver.instance.CurrencyProp.Amount.ToString();
    }
}
