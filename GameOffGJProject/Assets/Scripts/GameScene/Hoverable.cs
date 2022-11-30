using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(2, 10)]
    [SerializeField] private string displayedMessage;
    public string DisplayedMessage { set { displayedMessage = value; } }
    private TextMeshProUGUI messageText;
    BattleManager battleManager;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.TutorialScene) return;
        battleManager = BattleManager.Instance;
        messageText = GameObject.FindGameObjectWithTag("Message").GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.TutorialScene) return;
        //throw new System.NotImplementedException();
        if (battleManager.State == BattleState.PlayerTurn)
        {
            messageText.text = displayedMessage;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.TutorialScene) return;
        //throw new System.NotImplementedException();
        if (battleManager.State == BattleState.PlayerTurn)
        {
            messageText.text = " ";
        }
    }
}
