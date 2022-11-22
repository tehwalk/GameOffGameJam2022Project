using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardManager : MonoBehaviour
{
    private static CardManager instance;
    public static CardManager Instance
    {
        get
        {
            return instance;
        }
    }
    public List<Card> deck = new List<Card>();
    [HideInInspector] public List<Card> discarded = new List<Card>();
    [HideInInspector] public List<Card> selectedCards = new List<Card>();
    public RectTransform[] cardSlots;
    
    Dictionary<Transform, bool> availableSlots = new Dictionary<Transform, bool>();

    [Header("Text Properties")]
    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardedText;
    // Start is called before the first frame update
    [Header("Universal Card Properties")]
    public float playDisplacementDistance = 3;
    public float moveToDiscardPileTime = 2;

    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    void Start()
    {
        /*availableSlots = new bool[cardSlots.Length];
        for (int i = 0; i < availableSlots.Length; i++)
        {
            availableSlots[i] = true;
        }*/
        foreach(Transform t in cardSlots)
        {
            availableSlots.Add(t, true);
        }

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDeckSizeText();
        discardedText.text = "Discarded: " + discarded.Count.ToString();
    }

    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];
            foreach(Transform t in availableSlots.Keys)
            {
                if (availableSlots[t] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.hasBeenPlayed = false;
                    //randCard.gameObject.transform.position = Camera.main.WorldToScreenPoint(cardSlots[i].position);
                    randCard.transform.SetParent(t);
                    randCard.GetComponent<RectTransform>().position = t.position;
                    //randCard.gameObject.transform.rotation = cardSlots[i].rotation;
                    //randCard.handIndex = i;
                    availableSlots[t] = false;
                    deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    void UpdateDeckSizeText()
    {
        deckSizeText.text = "Deck: " + deck.Count.ToString();
    }

    public void Shuffle()
    {
        if (discarded.Count > 0)
        {
            foreach (Card d in discarded)
            {
                deck.Add(d);
            }
            discarded.Clear();
        }
    }

    public void DiscardSelectedCards()
    {
        foreach(Card c in selectedCards)
        {
            discarded.Add(c);
            availableSlots[c.transform.parent] = true;
            c.transform.SetParent(null);
            c.gameObject.SetActive(false);
        }

    }

    private void OnDrawGizmos() {
         //Gizmos.matrix = GameObject.FindObjectOfType<Canvas>().transform.localToWorldMatrix;
         Gizmos.color = Color.cyan;
         foreach(RectTransform r in cardSlots)
         {
            Gizmos.DrawSphere(r.position, 10f);
         }
    }
}
