using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
[RequireComponent(typeof(Hoverable))]
public class AbilityButton : MonoBehaviour
{
    Button myButton;
    public Ability myAbility;
    Player player;
    [SerializeField] PlayerAnimation playerAnimation;
    BattleManager battleManager;
    CardManager cardManager;
    Hoverable hoverable;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
        //playerAnimation = player.gameObject.GetComponentInChildren<PlayerAnimation>();
        battleManager = BattleManager.Instance;
        cardManager = CardManager.Instance;
        hoverable = GetComponent<Hoverable>();
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(delegate { UseAbility(); });
        DecorateButton();
        SetUpHoverableMessage();
    }

    void SetUpHoverableMessage()
    {
        hoverable.DisplayedMessage = myAbility.abilityName + ": " + myAbility.attackPoints.ToString() + " points.";
    }

    void DecorateButton()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = myAbility.abilityIcon;
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = myAbility.attackPoints.ToString();
        transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = myAbility.fireCost.ToString();
        transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = myAbility.airCost.ToString();
        transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = myAbility.earthCost.ToString();
        transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = myAbility.waterCost.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanAbilityBeUsed() == true)
        {
            myButton.interactable = true;
        }
        else
        {
            myButton.interactable = false;
        }
    }

    bool CanAbilityBeUsed()
    {
        if ((player.AirNumber >= myAbility.airCost && player.EarthNumber >= myAbility.earthCost)
          && (player.FireNumber >= myAbility.fireCost && player.WaterNumber >= myAbility.waterCost))
        {
            //Debug.Log("what");
            return true;
        }
        else return false;
    }

    void UseAbility()
    {
        AttackElement myAttack;
        myAttack.attackName = myAbility.abilityName;
        myAttack.attackDamage = myAbility.attackPoints;
        myAttack.abilityType = myAbility.abilityType;
        Debug.Log(myAbility.attackPoints + " in damage!");
        battleManager.PlayerAttack(myAttack);
        playerAnimation.PlayAnim();
        player.AirNumber -= myAbility.airCost;
        player.EarthNumber -= myAbility.earthCost;
        player.FireNumber -= myAbility.fireCost;
        player.WaterNumber -= myAbility.waterCost;
        cardManager.DiscardSelectedCards();
    }
}
