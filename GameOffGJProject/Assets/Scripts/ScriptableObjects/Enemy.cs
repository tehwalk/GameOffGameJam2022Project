using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueNode{
    public string dialogueName;
    [TextArea(3,10)] public string dialogueText;
};


[CreateAssetMenu(fileName ="NewEnemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
   public string enemyName;
   public int maxHealth;
   public Ability[] enemyAbilities;
   public GameObject enemyGFX;
   public GameObject backgroundPrefab;
   public AudioClip musicTrack;

   public DialogueNode[] introDialogueParts, outroDialogueParts;
}
