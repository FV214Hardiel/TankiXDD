using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    

    private void Start()
    {
        GameHandler.GameIsPaused = false;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameHandler.GameIsOver)
        {
            if (!GameHandler.GameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameHandler.GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameHandler.GameIsPaused = false;
    }

    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        GameHandler.GameIsPaused = false;
        SceneManager.LoadScene(sceneID);
        
    }
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
        GameHandler.GameIsPaused = false;

    }
}
