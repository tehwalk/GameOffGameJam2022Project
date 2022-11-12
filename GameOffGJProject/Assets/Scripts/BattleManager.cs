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
    EnemyBehaviour enemyBehaviour;
    public GameObject playerAttackPanel;
    //public Button drawButton, passButton;
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
        enemyBehaviour = enemyUnit.GetComponent<EnemyBehaviour>();
        state = BattleState.Start;
        //drawButton.interactable = false;
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
                //drawButton.interactable = true;
                playerAttackPanel.SetActive(true);
                break;
            case BattleState.EnemyTurn:
                //drawButton.interactable = false;
                playerAttackPanel.SetActive(false);
                //EnemyAttack(enemyAttackPoints);
                break;
            case BattleState.Won:
                //drawButton.interactable = false;
                playerAttackPanel.SetActive(false);
                //Debug.Log("Player won!!");
                dialogueText.text = "You won!!!";
                break;
            case BattleState.Lost:
                //drawButton.interactable = false;
                playerAttackPanel.SetActive(false);
                //Debug.Log("Player Lost!!");
                dialogueText.text = "You lost!!!";
                break;
            default:
                break;
        }
    }

    public void PlayerPass()
    {
        StartCoroutine(TransitionToState(BattleState.EnemyTurn, "You passed your turn!"));
    }

    public void PlayerAttack(AttackElement attack)
    {
        bool ded = enemyUnit.IsDead(attack.attackDamage);
        if (ded == true)
        {
            state = BattleState.Won;
        }
        else
        {
            //state = BattleState.EnemyTurn;
            StartCoroutine(TransitionToState(BattleState.EnemyTurn, "You attacked " + enemyUnit.unitName + " with " + attack.attackName));
            //EnemyAttack(enemyAttackPoints);
        }
    }

    public void EnemyAttack(AttackElement attack)
    {
        bool ded = playerUnit.IsDead(attack.attackDamage);
        if (ded == true)
        {
            state = BattleState.Lost;
        }
        else
        {
            StartCoroutine(TransitionToState(BattleState.PlayerTurn, enemyUnit.unitName + " has attacked you with " + attack.attackName));
        }
    }

    IEnumerator TransitionToState(BattleState desiredState, string message)
    {
        dialogueText.text = message;
        yield return new WaitForSeconds(intervalTime);
        state = desiredState;
        //testing purposes will probably change in the future
        if (desiredState == BattleState.EnemyTurn)
        {
            EnemyAttack(enemyBehaviour.EnemyRandomAttack());
        }
        else if (desiredState == BattleState.PlayerTurn)
        {
            dialogueText.text = "Pick your attack";
        }
    }

}
