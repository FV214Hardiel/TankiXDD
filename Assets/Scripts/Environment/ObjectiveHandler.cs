using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHandler : MonoBehaviour
{
    public int ObjectivesBaseCount;
    public GameObject winScreen;
    void Start()
    {
        
        ObjectivesBaseCount = 15;
        //ObjectivesBaseCount = GetComponentsInChildren<HealthBuildong>().GetLength(0);
        
        HealthBuildong.objectiveBuildingDestroyed += OnBuildingDestroy;
    }

    private void OnDestroy()
    {
       
        HealthBuildong.objectiveBuildingDestroyed -= OnBuildingDestroy;
    }
    void OnBuildingDestroy()
    {
        
        ObjectivesBaseCount -= 1;
        Debug.Log("on destroy" + ObjectivesBaseCount);
        //if (ObjectivesBaseCount == 0)
        //{
        //    OnObjectivesComplete();
            

        //}
       
    }

    void OnObjectivesComplete()
    {
        //Debug.Log("pobeda");        
        Time.timeScale = 1;
        StartCoroutine(LevelEnding());
        GameHandler.GameIsOver = true;
        GameHandler.GameIsPaused = true;
        winScreen.SetActive(true);

    }
    private IEnumerator LevelEnding()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 10; i >= 0; i--)
        {
            Time.timeScale = i/10f;
            yield return new WaitForSecondsRealtime(0.3f);
            //Debug.Log(Time.timeScale);
            
        }
    }

    public void OnFlagCapture()
    {
        OnObjectivesComplete();
    }
}
