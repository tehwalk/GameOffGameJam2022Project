using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DialogueEditor;

public class CutsceneControl : MonoBehaviour
{
    public SceneIndex desiredScene;
    [SerializeField] private float fadeTime;
    [SerializeField] private Image blackImage;
    NPCConversation convo;
    // Start is called before the first frame update
    private void OnEnable()
    {
        ConversationManager.OnConversationEnded += GoToScene;
    }

    private void OnDisable()
    {
        ConversationManager.OnConversationEnded -= GoToScene;
    }
    void Start()
    {
        convo = GetComponent<NPCConversation>();
        LeanTween.alpha(blackImage.rectTransform, 0, fadeTime * Time.deltaTime).setOnComplete(
        () =>
        {
            blackImage.gameObject.SetActive(false);
            ConversationManager.Instance.StartConversation(convo);

        }
    );

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GoToScene()
    {
        blackImage.gameObject.SetActive(true);
        LeanTween.alpha(blackImage.rectTransform, 1, fadeTime * Time.deltaTime).setOnComplete(
       () =>
       {
           SceneManager.LoadScene((int)desiredScene);
       }
   );

    }


}
