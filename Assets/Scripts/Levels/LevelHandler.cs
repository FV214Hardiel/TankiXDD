using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler instance;
    public List<string> teams; //List of teams on the level

    private void OnEnable()
    {
        instance = this;
    }

    
}
