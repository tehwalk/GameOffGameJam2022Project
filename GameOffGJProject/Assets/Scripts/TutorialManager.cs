using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TutorialStage{Introduction, AttackTest, BeatTest, HealTest, Outro}
public class TutorialManager : MonoBehaviour
{
    public SceneIndex desiredScene;
    [SerializeField] GameObject pausePanel;
    //public SpeechNode myNode;
    // Start is called before the first frame update
    void Start()
    {
        //conversation = convo.Deserialize();
        pausePanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        pausePanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenuScene);
    }

    public void GoToMap()
    {
        SceneManager.LoadScene((int)SceneIndex.MapScene);
    }
}
