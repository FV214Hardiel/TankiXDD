using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaunMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        //Debug.Log(ObjectiveHandler.ObjectivesBaseCount);
    }

  

    public void ExitGame()
    {
        Debug.Log("Gotovo");
       
        Application.Quit();
    }
}
