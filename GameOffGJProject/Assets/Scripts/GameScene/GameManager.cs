using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum GameState { Playing, Paused, GameOver }

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    GameState gameState;
    EnemyPass enemyPass;
    EnemyBehaviour enemyBehaviour;
    DialogueManager dialogueManager;
    public GameState GameState { get { return gameState; } }
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel, wonOptions, lostOptions;
    [SerializeField] private Image blackImage;
    [SerializeField] private float fadeTime;
    [SerializeField] AudioSource mainAudioSource;
    [SerializeField] AudioClip winSound, loseSound;
    bool isEndingDialogueActive = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null && _instance != this) _instance = null;
        _instance = this;
    }
    void Start()
    {
        Time.timeScale = 1;
        enemyPass = GameObject.FindGameObjectWithTag("EnemyPass").GetComponent<EnemyPass>();
        dialogueManager = DialogueManager.Instance;
        enemyBehaviour = EnemyBehaviour.Instance;
        gameState = GameState.Playing;
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        wonOptions.SetActive(false);
        lostOptions.SetActive(false);
        LeanTween.alpha(blackImage.rectTransform, 0, fadeTime * Time.deltaTime).setOnComplete(() =>
        {
            blackImage.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameState != GameState.GameOver) return;
        if (!isEndingDialogueActive) return;
        if (dialogueManager.myCoroutine == null)
        {
            GoToNextEnemy();
        }

    }
    #region Public Functions

    public void PauseGame()
    {
        Time.timeScale = 0;
        gameState = GameState.Paused;
        pausePanel.SetActive(true);
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        gameState = GameState.Playing;
        pausePanel.SetActive(false);
    }

    public void GameOver(bool won)
    {
        mainAudioSource.loop = false;
        Time.timeScale = 0;
        gameState = GameState.GameOver;
        gameOverPanel.SetActive(true);
        if (won == true)
        {
            wonOptions.SetActive(true);
            mainAudioSource.clip = winSound;
        }
        else
        {
            lostOptions.SetActive(true);
            mainAudioSource.clip = loseSound;
        }
        mainAudioSource.Play();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenuScene);
    }
    public void GoToNextEnemy()
    {
        Time.timeScale = 1;
        blackImage.gameObject.SetActive(true);
        LeanTween.alpha(blackImage.rectTransform, 0, fadeTime * Time.deltaTime).setOnComplete(() =>
        {
            if (enemyPass.AreAllWon() == false)
            {
                SceneManager.LoadScene((int)SceneIndex.MapScene);
            }
            else
            {
                SceneManager.LoadScene((int)SceneIndex.WinScene);
            }
        });
    }

    public void EndingDialogue()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        dialogueManager.ShowDialoguePanel();
        dialogueManager.ShowOutroDialogue();
        mainAudioSource.pitch = 0.85f;
        mainAudioSource.clip = enemyBehaviour.MyEnemyElements.musicTrack;
        mainAudioSource.Play();
        isEndingDialogueActive = true;
    }

    #endregion
}
