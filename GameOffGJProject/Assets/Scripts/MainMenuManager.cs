using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Image blackImage;
    [SerializeField] float fadeTime;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alpha(blackImage.rectTransform, 0, fadeTime).setOnComplete(() =>
        {
            blackImage.gameObject.SetActive(false);
        });
    }

    public void GoToMap()
    {
        blackImage.gameObject.SetActive(true);
        LeanTween.alpha(blackImage.rectTransform, 1, fadeTime).setOnComplete(() =>
            {
                SceneManager.LoadScene((int)SceneIndex.MapScene);
            });
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
