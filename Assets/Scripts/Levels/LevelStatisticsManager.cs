using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatisticsManager : MonoBehaviour
{
    public static LevelStatisticsManager instance;

    public Dictionary<string, float> dictOfStats;

    private void OnEnable()
    {
        instance = this;
    }
    void Start()
    {
        dictOfStats = new();
    }

    
    public void AddValue(string key, float value)
    {   
        if (dictOfStats.ContainsKey(key))
        {
            dictOfStats[key] += value;
            
        }
        else
        {
            dictOfStats.Add(key, value);
        }
        //print(key + ": " + dictOfStats[key]);

    }
}
