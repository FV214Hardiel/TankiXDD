using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ApplyButton : MonoBehaviour
{
    public TankHull chosenHull;
    public TankTurret chosenTurret;

    public TMP_Dropdown hullsDropdown;
    public TMP_Dropdown gunsDropdown;
    public TMP_Dropdown abilitiesDropdown;
    public TMP_Dropdown skinsDropdown;



    private void Start()
    {
       
    }

    public void ApplyClicked()
    {
        //Debug.Log(GameInfoSaver.instance.chosenHull);
        //Debug.Log(GameInfoSaver.instance.tanksList.allHulls[hullsDropdown.value]);
        GameInfoSaver.instance.chosenHull = GameInfoSaver.instance.tanksList.allHulls[hullsDropdown.value];
        Debug.Log("ApplyClickedHull + " + GameInfoSaver.instance.chosenHull);
        GameInfoSaver.instance.chosenTurret = GameInfoSaver.instance.tanksList.allTurrets[gunsDropdown.value];
        Debug.Log("ApplyClickedTurret + " + GameInfoSaver.instance.chosenTurret);
        GameInfoSaver.instance.chosenSkin = GameInfoSaver.instance.skins.unlockedSkins[skinsDropdown.value];
    }
}
