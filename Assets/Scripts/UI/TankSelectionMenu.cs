using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankSelectionMenu : MonoBehaviour
{
    public TMP_Dropdown hullsDropdown;
    public TMP_Dropdown gunsDropdown;    
    public List<TMP_Dropdown> abilitiesDropdowns;
    public TMP_Dropdown skinsDropdown;


    public Transform tankPreview;
    public TankHull hullPreview;
    public TankTurret turretPreview;
    public Texture2D skinPreview;
    public static List<int> abilitiesValues;

    GameObject createdPreview;

    // Start is called before the first frame update
    void Start()
    {
        abilitiesValues = new();
        foreach (TMP_Dropdown item in abilitiesDropdowns)
        {
            abilitiesValues.Add(item.value);
        }

        hullPreview = GameInfoSaver.instance.tanksList.unlockedHulls[0];
        turretPreview = GameInfoSaver.instance.tanksList.unlockedTurrets[0];
        skinPreview = GameInfoSaver.instance.skins.unlockedSkins[0];

        List<string> list = new();
        foreach (TankHull item in GameInfoSaver.instance.tanksList.unlockedHulls)
        {
            list.Add(item.hullName);
        }
        hullsDropdown.AddOptions(list);
        list.Clear();

        
        foreach (TankTurret item in GameInfoSaver.instance.tanksList.unlockedTurrets)
        {
            list.Add(item.gunName);
        }
        gunsDropdown.AddOptions(list);
        list.Clear();

        foreach (Texture2D item in GameInfoSaver.instance.skins.unlockedSkins)
        {
            if (item == null)
            {
                list.Add("No skin");
            }
            else { list.Add(item.name); }
            
        }
        skinsDropdown.AddOptions(list);
        list.Clear();

        foreach (AbilityCard item in GameInfoSaver.instance.abilitiesList.unlockedAbilities)
        {
            if (item == null)
            {
                list.Add("No ability");
            }
            else
            {
                list.Add(item.abilityName);
            }
        }
        foreach (TMP_Dropdown item in abilitiesDropdowns)
        {
            item.AddOptions(list);
        }
        
        list.Clear();

        createdPreview = AllHullsTurrets.CreateDecorative(tankPreview, hullPreview, 0, turretPreview, 0, skinPreview);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHull(int index)
    {
        Destroy(createdPreview);
        hullPreview = GameInfoSaver.instance.tanksList.unlockedHulls[index];
        createdPreview = AllHullsTurrets.CreateDecorative(tankPreview, hullPreview, 0, turretPreview, 0, skinPreview);
    }

    public void ChangeTurret(int index)
    {
        Destroy(createdPreview);
        turretPreview = GameInfoSaver.instance.tanksList.unlockedTurrets[index];
        createdPreview = AllHullsTurrets.CreateDecorative(tankPreview, hullPreview, 0, turretPreview, 0, skinPreview);
    }

    public void ChangeSkin(int index)
    {
        Destroy(createdPreview);
        skinPreview = GameInfoSaver.instance.skins.unlockedSkins[index];
        createdPreview = AllHullsTurrets.CreateDecorative(tankPreview, hullPreview, 0, turretPreview, 0, skinPreview);
    }
}
