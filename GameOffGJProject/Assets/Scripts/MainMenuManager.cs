using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] float fadeTime;
    [SerializeField] GameObject creditsPanel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        creditsPanel.SetActive(false);
        LeanTween.alpha(blackImage.rectTransform, 0, fadeTime * Time.deltaTime).setOnComplete(() =>
        {
            blackImage.gameObject.SetActive(false);
        });
    }

    public void GoToMap()
    {
        blackImage.gameObject.SetActive(true);
        LeanTween.alpha(blackImage.rectTransform, 1, fadeTime * Time.deltaTime).setOnComplete(() =>
            {
                SceneManager.LoadScene((int)SceneIndex.IntroScene);
            });
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenCreditsPanel()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
    }
}
