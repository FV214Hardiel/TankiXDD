using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Currency
{
    public ushort Amount { get; private set; }
    public event Action CurrencyChanged;

    public Currency()
    {
        LoadCurrency();
    }

    public void LoadCurrency()
    {
        if (PlayerPrefs.HasKey("uMoney"))
        {
            SetCurrency((ushort)PlayerPrefs.GetInt("uMoney"));
        }
        else
        {
            Amount = 0;
            SaveCurrency();
        }
    }

    public void SaveCurrency()
    {
        PlayerPrefs.SetInt("uMoney", Amount);
    }
    public void AddCurrency(ushort money)
    {
        Amount += money;
        CurrencyChanged?.Invoke();
    }
    public void SubtractCurrency(ushort money)
    {
        Amount -= money;
        CurrencyChanged?.Invoke();
    }
    public void SetCurrency(ushort money)
    {
        Amount = money;
        CurrencyChanged?.Invoke();
    }
}
