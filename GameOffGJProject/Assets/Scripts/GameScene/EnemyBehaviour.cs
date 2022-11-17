using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    Unit myUnit;
    Enemy myEnemyElements;
    EnemyPass enemyPass;
    // Start is called before the first frame update
    void Awake()
    {
        myUnit = GetComponent<Unit>();
        enemyPass = GameObject.FindGameObjectWithTag("EnemyPass").GetComponent<EnemyPass>();
        myEnemyElements = enemyPass.GetSelectedEnemy();
       // battleManager = BattleManager.Instance;
        myUnit.unitName = myEnemyElements.enemyName;
        myUnit.maxHealth = myEnemyElements.maxHealth;
    }

    private void Start() {
        GameObject enemyGFX = Instantiate(myEnemyElements.enemyGFX, transform.position, Quaternion.identity);
        enemyGFX.transform.SetParent(this.transform);
    }

    public AttackElement EnemyRandomAttack(){
        AttackElement myAttack;
        Ability selectedAbility = myEnemyElements.enemyAbilities[Random.Range(0, myEnemyElements.enemyAbilities.Length)];
        myAttack.attackName = selectedAbility.abilityName;
        myAttack.attackDamage = selectedAbility.attackPoints;
        myAttack.abilityType = selectedAbility.abilityType;
        return myAttack;
    }

    
}
