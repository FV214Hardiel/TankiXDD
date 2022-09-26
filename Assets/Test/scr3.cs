using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable IDE1006 // Naming Styles
public class scr3 : MonoBehaviour
#pragma warning restore IDE1006 // Naming Styles
{
    [SerializeField]
    int index;
    [SerializeField]
    string textString;

    public List<string> allTests;
    public List<string> unlockedTests;

    public int[] unlockedIndex;
    public string[] loadedString;

    void Start()
    {
        //print(PlayerPrefsX.GetStringArray("Popa").Length);


        unlockedTests = new();
        //foreach (int item in unlockedIndex)
        //{
        //    unlockedTests.Add(allTests[item]);
        //}

        

        //index = 1;
        //textString = "a";

        //int[] arr/* = new int[3] { 0, 3, 5 }*/;

        //PlayerPrefsX.SetIntArray("uSkins", arr);

        //arr = PlayerPrefsX.GetIntArray("uSkins");
        //Debug.Log(PlayerPrefsX.GetIntArray("uSkins")[2]);


    }

    

    public void Test()
    {
        unlockedTests.Clear();
        foreach (string item in loadedString)
        {
            unlockedTests.Add(allTests.Find(x => x == item));
        }

    }

    public void Save()
    {
        PlayerPrefsX.SetStringArray("uTests", unlockedTests.ToArray());
        print("Save");
    }

    public void Load()
    {
        loadedString = PlayerPrefsX.GetStringArray("uTests");
        print("Load");
        print(loadedString);

    }




}
