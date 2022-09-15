using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GunSelectButton : MonoBehaviour
{

    public TankTurret gunInfo;
    public GameObject infoPanel;
    public ApplyButton okbutton;
    TMPro.TextMeshProUGUI infoText;
    TMPro.TextMeshProUGUI nameText;
    
    //Image infoImage;
    void Start()
    {
        nameText = infoPanel.transform.Find("ModuleNameText").GetComponent<TMPro.TextMeshProUGUI>();
        infoText = infoPanel.transform.Find("InfoPanelText").GetComponent<TMPro.TextMeshProUGUI>();

    }

    

    public void ClickBlin()
    {       
        nameText.text = gunInfo.gunName;
        infoText.text = "Base DMG = " + gunInfo.dmg.ToString() + "\n" + gunInfo.turretRotationSpeed.ToString();

        okbutton.chosenTurret = gunInfo;       
    }


   
}
