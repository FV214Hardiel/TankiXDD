using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Balance : MonoBehaviour
{
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMP_Text>();
        GameInfoSaver.CurrencyChanged += ChangeText;
        ChangeText();
    }

    

    private void OnDisable()
    {
        GameInfoSaver.CurrencyChanged -= ChangeText;
    }

    void ChangeText()
    {
        text.text = ("You have " + GameInfoSaver.instance.Currency);
    }
}
