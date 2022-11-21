using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public TMPro.TMP_Dropdown levelsDropdown;

    public GameObject loadingScreen;
    public Slider loadingBar;

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
        string scosenSceneName = levelsDropdown.options[levelsDropdown.value].text;
        loadingScreen.SetActive(true);
        loadingBar.value = 0;        

        StartCoroutine(LoadSceneAsync(scosenSceneName));
    }

    public IEnumerator LoadSceneAsync(string sceneName)
    {
        yield return null;
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingBar.value = loadOperation.progress;

        while (!loadOperation.isDone)
        {
            //print(loadOperation.progress);
            loadingBar.value = loadOperation.progress;

            yield return null;
        }
    }
}
