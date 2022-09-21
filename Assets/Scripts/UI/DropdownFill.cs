using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownFill : MonoBehaviour
{
    public TMP_Dropdown hullsDropdown;
    public TMP_Dropdown gunsDropdown;
    public TMP_Dropdown abilitiesDropdown;
    public TMP_Dropdown skinsDropdown;

    // Start is called before the first frame update
    void Start()
    {
        List<string> list = new();
        foreach (TankHull item in GameInfoSaver.instance.tanksList.allHulls)
        {
            list.Add(item.hullName);
        }
        hullsDropdown.AddOptions(list);
        list.Clear();

        
        foreach (TankTurret item in GameInfoSaver.instance.tanksList.allTurrets)
        {
            list.Add(item.gunName);
        }
        gunsDropdown.AddOptions(list);
        list.Clear();

        foreach (Texture2D item in GameInfoSaver.instance.skins.unlockedSkins)
        {
            list.Add(item.name);
        }
        skinsDropdown.AddOptions(list);
        list.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
