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
    public SkinCard skinPreview;
    public static List<int> abilitiesValues;

    SelectMenuInfoPanelScript infoPanel;

    GameObject createdPreview;

    
    void OnEnable()
    {
        abilitiesValues = new();
        foreach (TMP_Dropdown item in abilitiesDropdowns)
        {
            abilitiesValues.Add(item.value);
        }       
        
        skinPreview = GameInfoSaver.instance.skinsList.unlockedSkins[0];


        //HULLS

        List<string> list = new();
        foreach (TankHull item in GameInfoSaver.instance.tanksList.unlockedHulls)
        {           
            list.Add(item.Name);
        }
        
        hullsDropdown.AddOptions(list);
        list.Clear();

        if (GameInfoSaver.instance.chosenHull != null)
        {
            int i = hullsDropdown.options.FindIndex(x => x.text == GameInfoSaver.instance.chosenHull.Name);
            //print(i);
            hullsDropdown.SetValueWithoutNotify(i);
            hullPreview = GameInfoSaver.instance.tanksList.unlockedHulls[i];
        }        
        else
        {
            hullPreview = GameInfoSaver.instance.tanksList.unlockedHulls[0];
        }


        //GUNS
        
        foreach (TankTurret item in GameInfoSaver.instance.tanksList.unlockedTurrets)
        {
            list.Add(item.Name);
        }
        gunsDropdown.AddOptions(list);
        list.Clear();

        if (GameInfoSaver.instance.chosenTurret != null)
        {
            int i = gunsDropdown.options.FindIndex(x => x.text == GameInfoSaver.instance.chosenTurret.Name);
            print(i);
            gunsDropdown.SetValueWithoutNotify(i);
            turretPreview = GameInfoSaver.instance.tanksList.unlockedTurrets[i];
        }
        else
        {
            turretPreview = GameInfoSaver.instance.tanksList.unlockedTurrets[0];
        }

        //SKINS

        foreach (SkinCard item in GameInfoSaver.instance.skinsList.unlockedSkins)
        {
            if (item == null)
            {
                list.Add("No skin");
            }
            else { list.Add(item.Name); }
            
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
            item.value = 0;
        }
        
        list.Clear();

        infoPanel = GetComponentInChildren<SelectMenuInfoPanelScript>(true);
        infoPanel.enabled = true;
        infoPanel.ChangeText(hullPreview);

        createdPreview = AllHullsTurrets.CreateDecorative(tankPreview, hullPreview, 0, turretPreview, 0, skinPreview);
    }
        

    public void ChangeHull(int index)
    {
        Destroy(createdPreview);
        hullPreview = GameInfoSaver.instance.tanksList.unlockedHulls[index];
        createdPreview = AllHullsTurrets.CreateDecorative(tankPreview, hullPreview, 0, turretPreview, 0, skinPreview);
        infoPanel.ChangeText(hullPreview);
    }

    public void ChangeTurret(int index)
    {
        Destroy(createdPreview);
        turretPreview = GameInfoSaver.instance.tanksList.unlockedTurrets[index];
        createdPreview = AllHullsTurrets.CreateDecorative(tankPreview, hullPreview, 0, turretPreview, 0, skinPreview);
        infoPanel.ChangeText(turretPreview);
    }

    public void ChangeSkin(int index)
    {
        Destroy(createdPreview);
        skinPreview = GameInfoSaver.instance.skinsList.unlockedSkins[index];
        createdPreview = AllHullsTurrets.CreateDecorative(tankPreview, hullPreview, 0, turretPreview, 0, skinPreview);
    }

    private void OnDisable()
    {
        hullsDropdown.ClearOptions();
        gunsDropdown.ClearOptions();
        skinsDropdown.ClearOptions();

        Destroy(createdPreview);

    }
}
