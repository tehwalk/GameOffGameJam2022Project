using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [HideInInspector] public bool hasBeenPlayed;
    private bool hasBeenPlaced = false;
    [HideInInspector] public int handIndex;
    public CardFace myCardFace;
    private CardManager manager;
    private Player player;
    TextMeshProUGUI cardTitle, cardDescription;
    private void Start()
    {
        manager = CardManager.Instance;
        player = Player.Instance;
        SetCardAppearance();
    }

    void SetCardAppearance()
    {
        transform.GetChild(6).GetComponent<SpriteRenderer>().sprite = myCardFace.detail1;
        transform.GetChild(8).GetComponent<SpriteRenderer>().sprite = myCardFace.cardFace;
        cardTitle = transform.GetChild(transform.childCount - 1).GetChild(0).GetComponent<TextMeshProUGUI>();
        cardDescription = transform.GetChild(transform.childCount - 1).GetChild(1).GetComponent<TextMeshProUGUI>();
        cardTitle.text = myCardFace.cardName;
        cardDescription.text = myCardFace.cardDescription;

    }

    private void OnMouseDown()
    {
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

    /*public void MoveToDiscardPile()
    {
        manager.discarded.Add(this);
        gameObject.SetActive(false);
    }*/
}
