using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsList", menuName = "Tanks/LevelsList")]
public class LevelsList : ScriptableObject
{
    //public List<string> allLevels;
    public List<string> levelNames;
    public List<int> levelIndexes;

    public List<string> unlockedLevels;

    //private void OnEnable()
    //{
    //    PlayerPrefs.DeleteKey("uLevels");
    //}

    public Dictionary<string, int> allLevelsDict;
    private void OnEnable()
    {
        allLevelsDict = new();
        for (int i = 0; i < levelNames.Count; i++)
        {
            allLevelsDict.Add(levelNames[i], levelIndexes[i]);
            //Debug.Log(levelNames[i] + ", index = " + levelIndexes[i]);
        }
        
    }

}
