using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewAbility", menuName = "Player Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public int attackPoints;
    public int waterCost, fireCost, earthCost, airCost;
    public Sprite abilityIcon;
}
