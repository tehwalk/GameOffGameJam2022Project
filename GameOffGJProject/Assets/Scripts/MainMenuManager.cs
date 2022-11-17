using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GoToMap()
    {
        SceneManager.LoadScene((int)SceneIndex.MapScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
