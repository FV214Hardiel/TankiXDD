using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler instance;

    public GameObject winScreen;
    public GameObject loseScreen;
    
    public bool GameIsPaused;
    public bool GameIsOver;
    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    void Start()
    {


        GameIsPaused = false;
        GameIsOver = false;
    }
    private void OnDestroy()
    {
        
    }
    
    
    
}
