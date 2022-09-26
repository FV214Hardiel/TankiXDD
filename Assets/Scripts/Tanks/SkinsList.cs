using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skins", menuName = "Tanks/Skins")]
public class SkinsList : ScriptableObject
{
    public List<Texture2D> allSkins;
    public List<Texture2D> unlockedSkins;
     


    //private void OnEnable()
    //{
    //    PlayerPrefs.DeleteKey("uSkins");
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
                unlockedSkins.Add(allSkins.Find(x => x.name == item));
            }
        }
        else
        {            
            List<string> textNames = new();
            
            foreach (Texture2D item in unlockedSkins)
            {
                if (item != null)
                {
                    textNames.Add(item.name);
                }
                
            }

            PlayerPrefsX.SetStringArray("uSkins", textNames.ToArray());
        }
    }

    public void Save()
    {

    }
}
