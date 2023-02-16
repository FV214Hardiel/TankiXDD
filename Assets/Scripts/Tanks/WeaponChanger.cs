using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class WeaponChanger : MonoBehaviour, ISpecialZone
{
    [SerializeField]
    TMP_Dropdown dropdown;
    [SerializeField]
    GameObject text;
    [SerializeField]
    GameObject menu;

    List<string> turrets = new List<string>();
    int index;

    bool tankInRange = false;
    
    void Start()
    {
       // input = GetComponent<InputAction>();

        int i = 0;
        foreach (TankTurret item in GameInfoSaver.instance.tanksList.unlockedTurrets)
        {
            if (GameInfoSaver.instance.chosenTurret == item)
                index = i;
            turrets.Add(item.Name);
            i++;
        }
        dropdown.AddOptions(turrets);
        dropdown.SetValueWithoutNotify(index);
    }

    public void Change()
    {
        index = dropdown.value;
        
            print("CHANGE)");
        //AllHullsTurrets.ChangePlayerTurret(Player.PlayerHull.transform, GameInfoSaver.instance.chosenTurret, 0);
        AllHullsTurrets.ChangePlayerTurret(Player.PlayerHull.transform, GameInfoSaver.instance.tanksList.unlockedTurrets[index], 0);


    }

    public void TriggerEntered(IEntity source)
    {
        text.SetActive(true);
        tankInRange= true;
        
    }

    public void TriggerExited(IEntity source)
    {
        text.SetActive(false);
        tankInRange = false;
        menu.SetActive(false);
        GameHandler.instance.GameIsPaused = false; Time.timeScale = 1f;

    }

    public void OpenClose(InputAction.CallbackContext context)
    {
        if (context.performed && tankInRange)
        {
            print("N PRESSED");
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                text.SetActive(true);
                GameHandler.instance.GameIsPaused = false; Time.timeScale = 1f;
            }
            else
            {
                menu.SetActive(true);
                text.SetActive(false);
                GameHandler.instance.GameIsPaused = true; Time.timeScale = 0.01f;
            }
           
            
        }
        
    }
}
