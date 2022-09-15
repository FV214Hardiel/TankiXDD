using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyButton : MonoBehaviour
{
    public TankHull chosenHull;
    public TankTurret chosenTurret;
    
    public void ApplyClicked()
    {
        Debug.Log("ApplyClickedDo" + chosenHull);
        GameInfoSaver.instance.chosenHull = chosenHull;
        Debug.Log("ApplyClickedHull");
        GameInfoSaver.instance.chosenTurret = chosenTurret;
        Debug.Log("ApplyClickedTurret");
    }
}
