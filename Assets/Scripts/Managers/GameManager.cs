using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool _paused = false;
    bool _gameOver;
    [SerializeField] PlayerStats _playerStats;
    [SerializeField] GameObject PauseScreen;
    [SerializeField] GameObject GameOverScreen;
    [SerializeField] private GameObject mouseGUI;

    private void Awake()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        if(_playerStats.HP <= 0)
        {
            _gameOver = true;
        }
        if (_gameOver)
        {
            GameOverScreen.SetActive(true);
            mouseGUI.SetActive(false);
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            GameOverScreen.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        GameOver();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void RestartGame()
    {
        _gameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        _gameOver = false;
        SceneManager.LoadScene(0);
    }
    public void PauseGame()
    {
        if (!_paused)
        {
            PauseScreen.SetActive(true);
            mouseGUI.SetActive(false);
            Cursor.visible = true;
            _paused = true;
            Time.timeScale = 0;
        }
        else
        {
            PauseScreen.SetActive(false);
            mouseGUI.SetActive(true);
            Cursor.visible = false;
            _paused = false;
            Time.timeScale = 1;
        }
    }
}
