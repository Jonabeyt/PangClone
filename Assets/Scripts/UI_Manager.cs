using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    [SerializeField]
    GameObject pauseGameMenu;
    [SerializeField]
    GameObject gameEndMenu;
    [SerializeField]
    TextMeshProUGUI gameEndText;

    public void ShowGameEndMenu(bool gameOver = true)
    {
        gameEndMenu.SetActive(true);
        if (gameOver)
        {
            gameEndText.text = "Game Over...";
        }
        else
        {
            gameEndText.text = "Good Job!";
        }
    }

    public void TogglePauseMenu()
    {
        pauseGameMenu.SetActive(!pauseGameMenu.activeInHierarchy);
    }
}
