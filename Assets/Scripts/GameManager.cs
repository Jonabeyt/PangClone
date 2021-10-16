using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int score;
    float gameTime;
    public bool gamePaused;
    [SerializeField]
    UI_Manager UImanager;

    public delegate void PauseAction();
    public static event PauseAction onPause;

    void OnEnable()
    {
        PlayerController.OnDeath += GameOver;
        Ball.OnHit += AddScore;
        Ball.onLastBallDeath += Victory;
    }
    void OnDisable()
    {
        PlayerController.OnDeath -= GameOver;
        Ball.OnHit -= AddScore;
        Ball.onLastBallDeath -= Victory;
    }

    void Start()
    {
        gameTime = 0;
        score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
        UpdateTimeUI();
    }

    public void UpdateTimeUI()
    {
        if (gamePaused)
        {
            return;
        }
        UImanager.timeText.text = ((int)gameTime).ToString("D3");
    }
    
    public void AddScore(int scoreToAdd = 10)
    {
        score += scoreToAdd;
        UImanager.scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        gamePaused = true;
        UImanager.ShowGameEndMenu();
    }
    public void Victory()
    {
        gamePaused = true;
        UImanager.ShowGameEndMenu(false);
    }

    public void PauseGame()
    {
        gamePaused = !gamePaused;
        onPause();
        UImanager.TogglePauseMenu();
    }


}
