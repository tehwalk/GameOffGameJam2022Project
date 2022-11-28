using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewEnemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
   public string enemyName;
   public int maxHealth;
   public Ability[] enemyAbilities;
   public GameObject enemyGFX;
   public GameObject backgroundPrefab;
   public GameObject musicPrefab;
}
