using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Abilities", menuName = "Tanks/AbilitiesList")]
public class AbilitiesList : ScriptableObject
{
    public List<AbilityCard> allAbilities;
    public List<AbilityCard> unlockedAbilities;

    //private void OnEnable()
    //{
    //    PlayerPrefs.DeleteKey("uAbilities");
    //}

    public void Load()
    {
        string[] uAbilities;

        if (PlayerPrefs.HasKey("uAbilities") && PlayerPrefsX.GetStringArray("uAbilities").Length != 0)
        {
            uAbilities = PlayerPrefsX.GetStringArray("uAbilities");
            unlockedAbilities = new();
            unlockedAbilities.Add(null);
            foreach (string item in uAbilities)
            {
                unlockedAbilities.Add(allAbilities.Find(x => x.name == item));
            }
        }
        else
        {
            List<string> textNames = new();

            foreach (AbilityCard item in unlockedAbilities)
            {
                if (item != null)
                {
                    textNames.Add(item.name);
                }

            }

            PlayerPrefsX.SetStringArray("uAbilities", textNames.ToArray());
        }
    }
}
