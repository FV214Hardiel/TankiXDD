using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skins", menuName = "Tanks/Skins")]
public class SkinsList : ScriptableObject
{

    public List<SkinCard> allSkins;
    public List<SkinCard> unlockedSkins;
     


    //private void OnEnable()
    //{
    //    PlayerPrefs.DeleteKey("uSkins");
    //    Debug.Log("uSkins deleted");
    //}
    public void Load()
    {
        string[] uSkins;

        if (PlayerPrefs.HasKey("uSkins") && PlayerPrefsX.GetStringArray("uSkins").Length != 0)
        {            
            uSkins = PlayerPrefsX.GetStringArray("uSkins");
            unlockedSkins = new();
            unlockedSkins.Add(null);
            foreach (string item in uSkins)
            {
                unlockedSkins.Add(allSkins.Find(x => x.Name == item));
            }
        }
        else
        {
            Save();
        }
    }

    public void Save()
    {
        List<string> textNames = new();

        foreach (SkinCard item in unlockedSkins)
        {
            if (item != null)
            {
                textNames.Add(item.Name);
            }

        }

        PlayerPrefsX.SetStringArray("uSkins", textNames.ToArray());
    }
}
