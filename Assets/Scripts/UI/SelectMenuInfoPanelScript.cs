using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectMenuInfoPanelScript : MonoBehaviour
{
    
    [SerializeField]
    TextMeshProUGUI textName;
    [SerializeField]
    TextMeshProUGUI textInfo;
    // Start is called before the first frame update
    void Start()
    {
        
        //textName.text = "active";
        //textInfo.text = "active";
    }

    public void ChangeText(TankHull hull)
    {
        textName.text = hull.Name;
        textInfo.text = "HP: " + hull.modifications[0].baseHP +
                    "\nShield: " + hull.modifications[0].baseSP +
                    "\nSpeed: " + hull.modifications[0].hullSpeed +
                    "\nRotation: " + hull.modifications[0].hullRotation;
    }

    public void ChangeText(TankTurret tur)
    {
        textName.text = tur.Name;
        textInfo.text = "Damage: " + tur.modifications[0].damage +
                    "\nDelay between shots: " + tur.modifications[0].delayBetweenShots +
                    "\nRange: " + tur.modifications[0].attackRange +
                    "\nRotation: " + tur.modifications[0].turretRotationSpeed;
    }
}
