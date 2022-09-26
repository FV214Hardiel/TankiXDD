using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public TMPro.TMP_Dropdown levelsDropdown;
    // Start is called before the first frame update
    void Start()
    {
        List<string> list = new();
        foreach (string item in GameInfoSaver.instance.levelsList.unlockedLevels)
        {
            list.Add(item);
        }
        levelsDropdown.AddOptions(list);
        list.Clear();
    }

    public void LoadScene()
    {
        int index = GameInfoSaver.instance.levelsList.allLevelsDict[levelsDropdown.options[levelsDropdown.value].text];
        //Debug.Log(index);
        SceneManager.LoadScene(index);
        //MaunMenu.PlayGame()
    }
}
