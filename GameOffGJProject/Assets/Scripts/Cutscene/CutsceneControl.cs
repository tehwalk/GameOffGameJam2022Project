using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CutsceneControl : MonoBehaviour
{
    [TextArea(3, 10)] public string[] textParts;
    public SceneIndex desiredScene;
    [SerializeField] private float typeTime, fadeTime;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Image blackImage;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.alpha(blackImage.rectTransform, 0, fadeTime * Time.deltaTime).setOnComplete(
        () =>
        {
            blackImage.gameObject.SetActive(false);
            StartCoroutine(TypeText());
        }
    );

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator TypeText()
    {
        for (int i = 0; i < textParts.Length; i++)
        {
            //Debug.Log("write");
            //yield return new WaitForSeconds(2f);
            foreach (char c in textParts[i].ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typeTime);
            }
            yield return new WaitForSeconds(typeTime * 2.5f);
            dialogueText.text = string.Empty;
        }
        GoToScene();
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
