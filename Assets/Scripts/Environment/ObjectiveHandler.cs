using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHandler : MonoBehaviour
{
    public int ObjectivesBaseCount;
   
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
        LevelHandler.instance.OnObjectivesComplete();
    }
}
