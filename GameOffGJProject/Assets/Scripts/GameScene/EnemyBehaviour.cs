using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    private static EnemyBehaviour instance;
    public static EnemyBehaviour Instance { get { return instance; } }
    Unit myUnit;
    Enemy myEnemyElements;
    public Enemy MyEnemyElements{get{return myEnemyElements;}}
    EnemyPass enemyPass;
    DialogueManager dialogueManager;
    [SerializeField] Enemy placeholderEnemy;
    [SerializeField] AudioSource mainAudioSource;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;

        myUnit = GetComponent<Unit>();
        SelectEnemy();
        // battleManager = BattleManager.Instance;
        myUnit.unitName = myEnemyElements.enemyName;
        myUnit.maxHealth = myEnemyElements.maxHealth;
        InstantiatePrefabs();
        dialogueManager = DialogueManager.Instance;
        dialogueManager.textPartsIntro = myEnemyElements.introDialogueParts;
        dialogueManager.textPartsOutro = myEnemyElements.outroDialogueParts;
    }

    void SelectEnemy()
    {
        if (!GameObject.FindGameObjectWithTag("EnemyPass"))
        {
            myEnemyElements = placeholderEnemy;
            return;
        }
        enemyPass = GameObject.FindGameObjectWithTag("EnemyPass").GetComponent<EnemyPass>();
        myEnemyElements = enemyPass.GetSelectedEnemy();
    }

    void InstantiatePrefabs()
    {
        GameObject enemyGFX = Instantiate(myEnemyElements.enemyGFX, transform.position, Quaternion.identity);
        enemyGFX.transform.SetParent(this.transform);
        Instantiate(myEnemyElements.backgroundPrefab);
        mainAudioSource.loop = true;
        mainAudioSource.pitch = 1f;
        mainAudioSource.clip = myEnemyElements.musicTrack;
        mainAudioSource.Play();
    }

    private void Start()
    {

    }

    public AttackElement EnemyRandomAttack()
    {
        AttackElement myAttack;
        Ability selectedAbility = myEnemyElements.enemyAbilities[Random.Range(0, myEnemyElements.enemyAbilities.Length)];
        myAttack.attackName = selectedAbility.abilityName;
        myAttack.attackDamage = selectedAbility.attackPoints;
        myAttack.abilityType = selectedAbility.abilityType;
        return myAttack;
    }

    public void MarkEnemyAsDefeated()
    {
        if (myEnemyElements == placeholderEnemy) return; //debug
        enemyPass.EnemyHasBeenWon();
    }


}
