using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInfoSaver : MonoBehaviour
{
    public static GameInfoSaver instance;

    public static string playerName;
    public string enterName;

    public AllHullsTurrets tanksList;
    public SkinsList skins;

    public Texture2D chosenSkin;

    public TankHull chosenHull;
    public TankTurret chosenTurret;

    public byte hullTier;
    public byte turretTier;

    
    public List<AbilityCard> chosenAbilities;

    void Awake()
    {
        //Debug.Log("awake");
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
        }

        GameObject lh = GameObject.Find("LevelHandler");
        if (lh != null)
        {

            lh.GetComponent<Player>().enabled = true;
        }
        DontDestroyOnLoad(gameObject);

        

    }

    
   
}
