using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hull", menuName = "Tanks/Turrets")]
public class TankTurret : ScriptableObject, IShopItem
{
    public string turretName;
    public string Name
    {
        get
        {
            return turretName;
        }

    }

    public ushort turretPrice;
    public ushort Price
    {
        get
        {
            return turretPrice;
        }
    }

    public ushort upgradePrice;   

    public GameObject prefabOfTurret;

    public List<TurretMod> modifications;

    public void BuyItem()
    {
        if (!GameInfoSaver.instance.tanksList.unlockedTurrets.Contains(this))
        {
            GameInfoSaver.instance.tanksList.unlockedTurrets.Add(this);
        }

        GameInfoSaver.instance.tanksList.SaveTurrets();
    }


}
