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
    public BattleState State { get { return state; } }
    public Unit playerUnit, enemyUnit;
    EnemyBehaviour enemyBehaviour;
    GameManager gameManager;
    CardManager cardManager;
    DialogueManager dialogueManager;
    public GameObject battleHUD, playerAttackPanel;
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
        battleHUD.SetActive(false);
        gameManager = GameManager.Instance;
        cardManager = CardManager.Instance;
        dialogueManager = DialogueManager.Instance;
        enemyBehaviour = enemyUnit.GetComponent<EnemyBehaviour>();
        state = BattleState.Start;
        playerAttackPanel.SetActive(false);
        dialogueManager.ShowIntroDialogue();

    }

    private void Update()
    {
        switch (state)
        {
            case BattleState.Start:
                if (dialogueManager.myCoroutine == null)
                {
                    //Debug.Log("intro finished");
                    SkipToBattle();
                }
                break;
            case BattleState.PlayerTurn:
                playerAttackPanel.SetActive(true);
                break;
            case BattleState.EnemyTurn:
                playerAttackPanel.SetActive(false);
                break;
            case BattleState.Won:
                battleHUD.SetActive(false);
                playerAttackPanel.SetActive(false);
                /*if (dialogueManager.myCoroutine == null)
                {
                    Debug.Log("outro finished");
                    gameManager.GoToMap();
                }*/
                break;
            case BattleState.Lost:
                battleHUD.SetActive(false);
                playerAttackPanel.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void PlayerPass()
    {
        cardManager.ShuffleSelected();
        StartCoroutine(TransitionToState(BattleState.EnemyTurn, "You passed your turn!"));
    }

    public void PlayerAttack(AttackElement attack)
    {
        //make the Player do shit
        //check if this shit has killed Enemy Unit
        MakeAction(attack, playerUnit, enemyUnit);
        if (enemyUnit.IsDead() == true)
        {
            enemyBehaviour.MarkEnemyAsDefeated();
            StartCoroutine(TransitionToState(BattleState.Won, "You used " + attack.attackName));
        }
        else
        {
            StartCoroutine(TransitionToState(BattleState.EnemyTurn, "You used " + attack.attackName));
        }
    }

    public void EnemyAttack(AttackElement attack)
    {
        //make the EnemyBehaviour do some shit
        //check if this shit has killed Player Unit
        MakeAction(attack, enemyUnit, playerUnit);
        if (playerUnit.IsDead() == true)
        {
            StartCoroutine(TransitionToState(BattleState.Lost, enemyUnit.unitName + " used " + attack.attackName));
        }
        else
        {
            StartCoroutine(TransitionToState(BattleState.PlayerTurn, enemyUnit.unitName + " used " + attack.attackName));
        }
    }

    public IEnumerator TransitionToState(BattleState desiredState, string message)
    {
        dialogueText.text = message;
        yield return new WaitForSeconds(intervalTime);
        state = desiredState;
        switch (state)
        {
            case BattleState.PlayerTurn:
                dialogueText.text = "Draw cards and pick your attack";
                break;
            case BattleState.EnemyTurn:
                EnemyAttack(enemyBehaviour.EnemyRandomAttack());
                break;
            case BattleState.Won:
                gameManager.GameOver(true);
                // ShowOutroDialogue();
                break;
            case BattleState.Lost:
                gameManager.GameOver(false);
                //ShowOutroDialogue();
                break;
            default:
                break;
        }
    }

    ///<summary>Makes the units make and accept an action (attack), depending on the type of the attack, the unit which caused it (causer) and the unit which accepted the attack (victim).</summary>
    void MakeAction(AttackElement attackElement, Unit causer, Unit victim)
    {
        switch (attackElement.abilityType)
        {
            case AbilityType.Attack:
                victim.TakeDamage(attackElement.attackDamage);
                break;
            case AbilityType.Heal:
                causer.Heal(attackElement.attackDamage);
                break;
            default:
                Debug.LogError("This type is not configured.");
                break;
        }
    }

    public void SkipToBattle()
    {
        battleHUD.SetActive(true);
        dialogueManager.HideDialoguePanel();
        StartCoroutine(TransitionToState(BattleState.PlayerTurn, enemyUnit.unitName + " wants to battle!"));
    }

    void ShowOutroDialogue()
    {
        battleHUD.SetActive(false);
        dialogueManager.ShowDialoguePanel();
        dialogueManager.ShowOutroDialogue();
    }

    public void SkipToWin()
    {
        battleHUD.SetActive(false);
        gameManager.GameOver(true);
    }

}
