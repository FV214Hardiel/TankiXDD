using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static LevelHandler instance;
    public List<string> teams; //List of teams on the level

    public float score;

    public byte lives;

    //Timer
    float timer;

    public int seconds;
    public int minutes;

    public string timerText;

    void Start()
    {
        timer = 0;

        HealthPlayer.playerIsDead += OnPlayersDeath;
        lives = 3;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }
    public void AddScore(float addedScore)
    {
        score += addedScore;
    }
    public float TransformScoreToCurrency(float score)
    {
        score = Mathf.Clamp(score, 0, 1000);

        return score * 0.1f;
    }
    void OnPlayersDeath()
    {
        if (lives > 0)
        {
            StartCoroutine(Player.instance.Respawn(5));
            lives--;

        }
        else
        {
            GameHandler.instance.GameIsOver = true;
            GameHandler.instance.GameIsPaused = true;
            GameHandler.instance.loseScreen.SetActive(true);
            Time.timeScale = 0.8f;
            StartCoroutine(LevelEnding());
        }


    }
    public void OnObjectivesComplete()
    {
        //Debug.Log("pobeda");        
        Time.timeScale = 1;
        StartCoroutine(LevelEnding());
        GameHandler.instance.GameIsOver = true;
        GameHandler.instance.GameIsPaused = true;
        GameHandler.instance.winScreen.SetActive(true);
        ushort earnedMoney = (ushort)Mathf.FloorToInt(TransformScoreToCurrency(score));
        GameInfoSaver.instance.AddCurrency(earnedMoney);

    }

    private IEnumerator LevelEnding()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 8; i >= 0; i--)
        {
            Time.timeScale = i / 10f;
            yield return new WaitForSecondsRealtime(0.3f);


        }
    }

    private void Awake()
    {
        instance = this;
    }



    private void OnDisable()
    {
        instance = null;
    }


}
