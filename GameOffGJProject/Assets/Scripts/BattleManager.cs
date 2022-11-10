using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { Start, PlayerTurn, EnemyTurn, Won, Lost }
public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance
    {
        get { return instance; }
    }
    BattleState state;
    public Unit playerUnit, enemyUnit;
    public GameObject playerAttackPanel;
    public Button drawButton;
    public TextMeshProUGUI dialogueText;
    [SerializeField] private float intervalTime;
    [SerializeField] private int enemyAttackPoints;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    void Start()
    {
        state = BattleState.Start;
        drawButton.interactable = false;
        playerAttackPanel.SetActive(false);
        //StartCoroutine(BeginBattle());
        StartCoroutine(TransitionToState(BattleState.PlayerTurn, "A wild " + enemyUnit.unitName + " has appeared!!"));
    }

    IEnumerator BeginBattle()
    {
        dialogueText.text = "A wild " + enemyUnit.unitName + " has appeared!!";
        yield return new WaitForSeconds(intervalTime);
        state = BattleState.PlayerTurn;
    }
    private void Update()
    {
        switch (state)
        {
            case BattleState.PlayerTurn:
                drawButton.interactable = true;
                playerAttackPanel.SetActive(true);
                break;
            case BattleState.EnemyTurn:
                drawButton.interactable = false;
                playerAttackPanel.SetActive(false);
                //EnemyAttack(enemyAttackPoints);
                break;
            case BattleState.Won:
                drawButton.interactable = false;
                playerAttackPanel.SetActive(false);
                //Debug.Log("Player won!!");
                dialogueText.text = "You won!!!";
                break;
            case BattleState.Lost:
                drawButton.interactable = false;
                playerAttackPanel.SetActive(false);
                //Debug.Log("Player Lost!!");
                dialogueText.text = "You lost!!!";
                break;
            default:
                break;
        }
    }

    public void PlayerAttack(int damage)
    {
        bool ded = enemyUnit.IsDead(damage);
        if (ded == true)
        {
            state = BattleState.Won;
        }
        else
        {
            //state = BattleState.EnemyTurn;
            StartCoroutine(TransitionToState(BattleState.EnemyTurn, "You attacked " + enemyUnit.unitName + " with " + damage.ToString() + " points of damage!"));
            //EnemyAttack(enemyAttackPoints);
        }
    }

    public void EnemyAttack(int damage)
    {
        bool ded = playerUnit.IsDead(damage);
        if (ded == true)
        {
            state = BattleState.Lost;
        }
        else
        {
            StartCoroutine(TransitionToState(BattleState.PlayerTurn, enemyUnit.unitName + " has attacked you with " + damage.ToString() + " points of damage!"));
        }
    }

    IEnumerator TransitionToState(BattleState desiredState, string message)
    {
        dialogueText.text = message;
        yield return new WaitForSeconds(intervalTime);
        state = desiredState;
        //testing purposes will probably change in the future
        if(desiredState == BattleState.EnemyTurn)
        {
            EnemyAttack(enemyAttackPoints);
        }
        else if(desiredState == BattleState.PlayerTurn)
        {
            dialogueText.text = "Pick your attack";
        }
    }

}
