using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    // Start is called before the first frame update
    private void Start()
    {
        settingsPanel.gameObject.SetActive(false);
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(true);
    }

    public void ClosedSettingsPanel()
    {
        settingsPanel.gameObject.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        SceneManager.LoadScene((int)SceneIndex.MainMenuScene);
    }
}
