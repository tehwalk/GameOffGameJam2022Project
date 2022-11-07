using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class AbilityButton : MonoBehaviour
{
    Button myButton;
    public Ability myAbility;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Player.Instance;
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(delegate { UseAbility(); });
        DecorateButton();
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
        Debug.Log(myAbility.attackPoints + " in damage!");
    }
}