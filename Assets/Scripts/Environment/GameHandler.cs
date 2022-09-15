using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject loseScreen;
    
    public static bool GameIsPaused;
    public bool pausa = GameIsPaused;
    public static bool GameIsOver;
    public bool geymOver = GameIsOver;
    void Start()
    {

        GameIsPaused = false;
        GameIsOver = false;
        HealthPlayer.playerIsDead += OnPlayersDeath;
    }
    private void OnDestroy()
    {

        HealthPlayer.playerIsDead -= OnPlayersDeath;
    }
    void OnPlayersDeath()
    {
        GameHandler.GameIsOver = true;
        GameHandler.GameIsPaused = true;
        loseScreen.SetActive(true);
        Time.timeScale = 0.8f;
        StartCoroutine(LevelEnding());
    }
    // Update is called once per frame
    private IEnumerator LevelEnding()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 8; i >= 0; i--)
        {
            Time.timeScale = i / 10f;
            yield return new WaitForSecondsRealtime(0.3f);
            //Debug.Log(Time.timeScale);

        }
    }
}
