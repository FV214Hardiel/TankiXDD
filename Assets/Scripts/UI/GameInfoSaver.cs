using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfoSaver : MonoBehaviour
{
    //¡≈«”ÃÕŒ ÃŒ∆ÕŒ ¡€“‹ œ≈–¬€Ã
    public static GameInfoSaver instance;

    public static string playerName;
    public string enterName;

    public Currency CurrencyProp { get; private set; }

    [Space]
    public LevelsList levelsList;    
    public AllHullsTurrets tanksList;
    public SkinsList skinsList;
    public AbilitiesList abilitiesList;

    [Space]   
    public TankHull chosenHull;
    public TankTurret chosenTurret;
    public SkinCard chosenSkin;
    public List<AbilityCard> chosenAbilities;

    [Space]
    public byte hullTier;
    public byte turretTier;
       
    


    void Awake()
    {
         
        //Debug.Log("TEST");
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {           
            instance = this;
            Load();
        }

        //GameObject aui = GameObject.Find("AbilitiesUI");
        //if (aui != null)
        //{
        //    aui.GetComponent<UIAbilityHandler>().enabled = true;
        //}

        GameObject lh = GameObject.Find("LevelHandler");
        if (lh != null)
        {
            lh.GetComponent<LevelHandler>().enabled = true;

            lh.GetComponent<Player>().enabled = true;
        }

        

        DontDestroyOnLoad(gameObject);       

    }

    private void OnDisable()
    {
        //SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //public void AddCurrency(ushort money)
    //{
    //    Currency += money;
    //    CurrencyChanged?.Invoke();
    //}
    //public void SubtractCurrency(ushort money)
    //{
    //    Currency -= money;
    //    CurrencyChanged?.Invoke();
    //}
    //public void SetCurrency(ushort money)
    //{
    //    Currency = money;
    //    CurrencyChanged?.Invoke();
    //}



    //public void Test()
    //{
    //    AddCurrency(45);
    //}

    public void Load()
    {
        CurrencyProp = new Currency();
        tanksList.LoadHulls();
        tanksList.LoadTurrets();

        skinsList.Load();

        abilitiesList.Load();
    }
    public void SaveGame()
    {        
        CurrencyProp.SaveCurrency();

        tanksList.SaveHulls();
        tanksList.SaveTurrets();

        skinsList.Save();

        abilitiesList.Save();
    }

   
}
