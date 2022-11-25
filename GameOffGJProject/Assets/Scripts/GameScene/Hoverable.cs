using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
        messageText = GameObject.FindGameObjectWithTag("Message").GetComponent<TextMeshProUGUI>();
        battleManager = BattleManager.Instance;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if (battleManager.State == BattleState.PlayerTurn)
        {
            messageText.text = displayedMessage;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if (battleManager.State == BattleState.PlayerTurn)
        {
            messageText.text = " ";
        }
    }
}
