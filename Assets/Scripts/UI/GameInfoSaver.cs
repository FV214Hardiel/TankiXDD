using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfoSaver : MonoBehaviour
{
    public static GameInfoSaver instance;

    public static string playerName;
    public string enterName;

    public ushort Currency { get; private set; }
    public static event Action CurrencyChanged;

    [Space]
    public LevelsList levelsList;    
    public AllHullsTurrets tanksList;
    public SkinsList skinsList;
    public AbilitiesList abilitiesList;

    [Space]   
    public TankHull chosenHull;
    public TankTurret chosenTurret;
    public Texture2D chosenSkin;
    public List<AbilityCard> chosenAbilities;

    [Space]
    public byte hullTier;
    public byte turretTier;
       
    


    void Awake()
    {
               
        //Debug.Log(enterName);
        //playerName = enterName;
        //Debug.Log(playerName);
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {           
            instance = this;
            Load();
        }

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
        SaveGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SetCurrency(ushort money)
    {
        Currency += money;
        CurrencyChanged?.Invoke();
    }

    public void Test()
    {
        SetCurrency(15);
    }

    void Load()
    {
        LoadCurrency();

        tanksList.LoadHulls();
        tanksList.LoadTurrets();

        skinsList.Load();

        abilitiesList.Load();
    }
    public void SaveGame()
    {
        SaveCurrency();

        tanksList.SaveHulls();
        tanksList.SaveTurrets();

        skinsList.Save();

        abilitiesList.Save();
    }

    void LoadCurrency()
    {
        //int uMoney;

        if (PlayerPrefs.HasKey("uMoney"))
        {
            SetCurrency((ushort)PlayerPrefs.GetInt("uMoney"));
        }
        else
        {
            SaveCurrency();
        }
    }

    void SaveCurrency()
    {
        PlayerPrefs.SetInt("uMoney", Currency);
    }
   
}
