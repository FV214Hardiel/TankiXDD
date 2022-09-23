using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaunMenu : MonoBehaviour
{
    public static void PlayGame(int index)
    {
        SceneManager.LoadScene(index);
        //Debug.Log(ObjectiveHandler.ObjectivesBaseCount);
    }

  

    public void ExitGame()
    {
        Debug.Log("Gotovo");
       
        Application.Quit();
    }
}
