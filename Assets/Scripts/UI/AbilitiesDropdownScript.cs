using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitiesDropdownScript : MonoBehaviour
{
    public int sourceIndex;
    public List<TMP_Dropdown> allDDs;
    public List<int> oldValues;
    void Start()
    {
        
    }
        
    void Update()
    {
        
    }

    public void Test(int newValue)
    {
        

        for (int i = 0; i < 5; i++)
        {
            if (TankSelectionMenu.abilitiesValues[i] == newValue && i != sourceIndex && newValue != 0)
            {
                Debug.Log("In " + i);
                allDDs[i].SetValueWithoutNotify(TankSelectionMenu.abilitiesValues[sourceIndex]);

            }
        }
        TankSelectionMenu.abilitiesValues.Clear();
        foreach (TMP_Dropdown item in allDDs)
        {
            TankSelectionMenu.abilitiesValues.Add(item.value);
        }

        //Debug.Log("puk");
    }
}
