using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenPausePanel()
    {
        pausePanel.gameObject.SetActive(true);
    }

    public void ClosePausePanel()
    {
        pausePanel.gameObject.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenuScene);
    }
}
