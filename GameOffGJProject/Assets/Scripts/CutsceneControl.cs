using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneControl : MonoBehaviour
{
    public SceneIndex desiredScene;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToScene()
    {
        SceneManager.LoadScene((int)desiredScene);
    }


}
