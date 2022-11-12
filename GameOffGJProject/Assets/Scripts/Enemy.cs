using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
   public string enemyName;
   public Ability[] enemyAbilities;
}
