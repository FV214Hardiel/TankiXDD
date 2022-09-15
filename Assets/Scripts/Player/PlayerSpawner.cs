using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public TankHull tankHull;
    public TankTurret tankTurret;

    void Awake()
    {
        tankHull = GameInfoSaver.instance.chosenHull;
        tankTurret = GameInfoSaver.instance.chosenTurret;

    }

    
}
