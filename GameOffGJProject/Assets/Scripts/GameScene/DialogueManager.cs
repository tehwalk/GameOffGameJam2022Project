using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance { get { return instance; } }
    public DialogueNode[] textPartsIntro;
    public DialogueNode[] textPartsOutro;
    [SerializeField] GameObject storyPanel;
    [SerializeField] Button skipButton;
    [SerializeField] private float typeTime, intervalTime;
    [SerializeField] private TextMeshProUGUI dialogueName, dialogueText;
    bool introFinished, outroFinished;
    public IEnumerator myCoroutine;
    BattleManager battleManager;
    GameManager gameManager;

    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;

        battleManager = BattleManager.Instance;
        gameManager = GameManager.Instance;
    }

    IEnumerator TypeText(DialogueNode[] textParts, bool check)
    {

        for (int i = 0; i < textParts.Length; i++)
        {
            dialogueName.text = textParts[i].dialogueName;
            //Debug.Log("write");
            //yield return new WaitForSeconds(2f);
            foreach (char c in textParts[i].dialogueText.ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typeTime);
            }
            yield return new WaitForSeconds(intervalTime);
            dialogueText.text = string.Empty;
        }
        myCoroutine = null;
    }

    public void ShowIntroDialogue()
    {

        myCoroutine = TypeText(textPartsIntro, introFinished);
        StartCoroutine(myCoroutine);
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(delegate { battleManager.SkipToBattle(); });
    }

    public void ShowOutroDialogue()
    {
        myCoroutine = TypeText(textPartsOutro, outroFinished);
        StartCoroutine(myCoroutine);
        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(delegate { gameManager.GoToNextEnemy(); });
    }

    public void ShowDialoguePanel()
    {
        storyPanel.SetActive(true);
    }

    public void HideDialoguePanel()
    {
        storyPanel.SetActive(false);
    }



}
