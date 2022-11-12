using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyBehaviour : MonoBehaviour
{
    Unit myUnit;
    public Enemy myEnemyElements;
    //public GameObject myHUD;
   // BattleManager battleManager;
    // Start is called before the first frame update
    void Start()
    {
        myUnit = GetComponent<Unit>();
       // battleManager = BattleManager.Instance;
        myUnit.unitName = myEnemyElements.enemyName;
    }

    public AttackElement EnemyRandomAttack(){
        AttackElement myAttack;
        Ability selectedAbility = myEnemyElements.enemyAbilities[Random.Range(0, myEnemyElements.enemyAbilities.Length)];
        myAttack.attackName = selectedAbility.abilityName;
        myAttack.attackDamage = selectedAbility.attackPoints;
        return myAttack;
    }

    
}
