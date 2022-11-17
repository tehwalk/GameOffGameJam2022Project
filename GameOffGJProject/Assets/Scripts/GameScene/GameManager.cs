using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GameState { Playing, Paused, GameOver }

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    GameState gameState;
    public GameState GameState { get { return gameState; } }
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel, wonOptions, lostOptions;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this) _instance = null;
        _instance = this;
    }
    void Start()
    {
        gameState = GameState.Playing;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        wonOptions.SetActive(false);
        lostOptions.SetActive(false);
    }

    // Update is called once per frame
    #region Public Functions

    public void PauseGame()
    {
        gameState = GameState.Paused;
        pausePanel.SetActive(true);
    }

    public void UnPauseGame()
    {
        gameState = GameState.Playing;
        pausePanel.SetActive(false);
    }

    public void GameOver(bool won)
    {
        gameState = GameState.GameOver;
        gameOverPanel.SetActive(true);
        if (won == true)
        {
            wonOptions.SetActive(true);
        }
        else
        {
            lostOptions.SetActive(true);
        }
    }
    public void GoToMap()
    {
        SceneManager.LoadScene((int)SceneIndex.MapScene);
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenuScene);
    }

    #endregion
}
