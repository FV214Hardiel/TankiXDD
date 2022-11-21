using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skin", menuName = "Tanks/SkinCard")]
public class SkinCard : ScriptableObject, IShopItem
{
    public string skinName;
    public string Name
    {
        get
        {
            return skinName;
        }       

    }

    public ushort skinPrice;
    public ushort Price {
        get
        {
            return skinPrice;
        }
    }

    public Texture2D skinTexture;
    

    public void BuyItem()
    {
        
        if (!GameInfoSaver.instance.skinsList.unlockedSkins.Contains(this))
        {
            GameInfoSaver.instance.skinsList.unlockedSkins.Add(this);
        }

        GameInfoSaver.instance.skinsList.Save();
    }
}
