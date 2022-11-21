using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsList", menuName = "Tanks/LevelsList")]
public class LevelsList : ScriptableObject
{
    
    public List<string> allLevels;   

    public List<string> unlockedLevels;

    private void OnEnable()
    {
        PlayerPrefs.DeleteKey("uLevels");
    }




}
