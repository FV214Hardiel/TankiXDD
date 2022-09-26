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
            tanksList.LoadHulls();
            tanksList.LoadTurrets();

            skinsList.Load();
            
            abilitiesList.Load();
        }

        GameObject lh = GameObject.Find("LevelHandler");
        if (lh != null)
        {

            lh.GetComponent<Player>().enabled = true;
        }
        DontDestroyOnLoad(gameObject);       

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
   
}
