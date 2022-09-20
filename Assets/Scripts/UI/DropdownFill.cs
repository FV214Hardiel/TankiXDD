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

    // Start is called before the first frame update
    void Start()
    {
        List<string> tanksList = new();
        foreach (TankHull item in GameInfoSaver.instance.tanksList.allHulls)
        {
            tanksList.Add(item.hullName);
        }
        hullsDropdown.AddOptions(tanksList);
        tanksList.Clear();

        
        foreach (TankTurret item in GameInfoSaver.instance.tanksList.allTurrets)
        {
            tanksList.Add(item.gunName);
        }
        gunsDropdown.AddOptions(tanksList);
        tanksList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
