using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balance : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
        GameInfoSaver.instance.CurrencyProp.CurrencyChanged += ChangeText;
        ChangeText();
    }

    

    private void OnDisable()
    {
        GameInfoSaver.instance.CurrencyProp.CurrencyChanged -= ChangeText;
    }

    void ChangeText()
    {
        text.text = ("You have " + GameInfoSaver.instance.CurrencyProp.Amount);
    }
}
