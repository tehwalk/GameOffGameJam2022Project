using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType{Attack, Heal};

[CreateAssetMenu(fileName ="NewAbility", menuName = "Player Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public AbilityType abilityType;
    public int attackPoints;
    public int waterCost, fireCost, earthCost, airCost;
    public Sprite abilityIcon;
}
