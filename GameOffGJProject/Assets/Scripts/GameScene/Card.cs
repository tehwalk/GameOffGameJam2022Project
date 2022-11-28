using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[ExecuteInEditMode]
public class Card : MonoBehaviour, IPointerClickHandler
{
    //[HideInInspector] public bool hasBeenPlayed;
    private bool hasBeenPlaced = false;
    public bool HasBeenPlaced { get { return hasBeenPlaced; } set { hasBeenPlaced = value; } }
    [HideInInspector] public int handIndex;
    public CardFace myCardFace;
    private CardManager manager;
    private Player player;
    TextMeshProUGUI cardTitle, cardElementNumber;
    private void Start()
    {
        manager = CardManager.Instance;
        player = Player.Instance;
        SetCardAppearance();
    }

    void SetCardAppearance()
    {
        transform.GetChild(3).GetComponent<Image>().sprite = myCardFace.detail1;
        transform.GetChild(2).GetComponent<Image>().sprite = myCardFace.cardFace;
        cardTitle = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        cardTitle.text = myCardFace.cardName;
        cardElementNumber = transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        cardElementNumber.text = myCardFace.elementNumberGiven.ToString();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        hasBeenPlaced = !hasBeenPlaced;
        if (hasBeenPlaced == true)
        {
            transform.position += Vector3.up * manager.playDisplacementDistance;
            manager.selectedCards.Add(this);
            player.AddElementsFromCard(this);
        }
        else
        {
            transform.position -= Vector3.up * manager.playDisplacementDistance;
            manager.selectedCards.Remove(this);
            player.RemoveElementsFromCard(this);
        }
    }

}
