using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Hull", menuName = "Tanks/Hulls")]
public class TankHull : ScriptableObject, IShopItem
{
    public string hullName;
    public string Name
    {
        get
        {
            return hullName;
        }

    }

    public ushort hullPrice;
    public ushort Price
    {
        get
        {
            return hullPrice;
        }
    }

    public ushort upgradePrice;
    
    public GameObject prefabOfHull;

    public float baseHP;
    public float hullSpeed;
    public float hullRotation;

    public float baseSP;
    public float shieldRechargeRate;
    public float shieldRechargeDelay;

    public List<HullMod> modifications;

    public List<AudioClip> voiceover;

    public void BuyItem()
    {
        if (!GameInfoSaver.instance.tanksList.unlockedHulls.Contains(this))
        {
            GameInfoSaver.instance.tanksList.unlockedHulls.Add(this);
        }

        GameInfoSaver.instance.tanksList.SaveHulls();

        Debug.Log("Item bought");
    }


}
