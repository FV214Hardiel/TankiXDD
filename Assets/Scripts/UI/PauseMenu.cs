using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    PlayerInputActions inputActions;

    private void Start()
    {
        GameHandler.instance.GameIsPaused = false;

        inputActions = new();
        inputActions.PlayerTankControl.Enable();
    }

    public void Update()
    {
        if (inputActions.PlayerTankControl.Pause.WasPressedThisFrame() && !GameHandler.instance.GameIsOver) Pause();        

        if (inputActions.PauseMenu.ExitPause.WasPressedThisFrame() && !GameHandler.instance.GameIsOver) Resume();
    }

    
    public void Pause()
    {
        pauseMenu.SetActive(true);

        //Changing an input scheme
        inputActions.PlayerTankControl.Disable();
        inputActions.PauseMenu.Enable();

        //Stopping time
        Time.timeScale = 0f;
        GameHandler.instance.GameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);

        //Changing an input scheme
        inputActions.PauseMenu.Disable();
        inputActions.PlayerTankControl.Enable();

        //Unstopping time
        Time.timeScale = 1f;
        GameHandler.instance.GameIsPaused = false;
    }

    //Exiting to main menu
    public void Home(int sceneID)
    {
        Time.timeScale = 1f;
        GameHandler.instance.GameIsPaused = false;
        SceneManager.LoadScene(sceneID);
        
    }
    
    public void RestartLevel()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
        GameHandler.instance.GameIsPaused = false;

    }
}
