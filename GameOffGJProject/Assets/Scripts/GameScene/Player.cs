using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            return instance;
        }
    }
    private CardManager cardManager;
    private EnemyPass enemyPass;
    private int waterNumber, airNumber, fireNumber, earthNumber;
    public int WaterNumber { get { return waterNumber; } set {waterNumber = value;}}
    public int AirNumber { get { return airNumber; } set {airNumber = value;}}
    public int FireNumber { get { return fireNumber; } set {fireNumber = value;}}
    public int EarthNumber { get { return earthNumber; } set {earthNumber = value;}}

    [Header("UI Elements")]
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI airText; 
    public TextMeshProUGUI fireText;
    public TextMeshProUGUI earthText;
    [Header("Player Abilities")]
    [SerializeField] private AbilityButton[] abilities;

    private void Awake()
    {
        if (instance != null && instance != this) instance = null;
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        cardManager = CardManager.Instance;
        enemyPass = GameObject.FindGameObjectWithTag("EnemyPass").GetComponent<EnemyPass>();
        InitializeAbilities();
    }

    void InitializeAbilities()
    {
        foreach(AbilityButton b in abilities)
        {
            b.gameObject.SetActive(false);
        }
        int level = 0;
        foreach(Enemy e in enemyPass.DefeatedEnemies.Keys)
        {
           if(enemyPass.DefeatedEnemies[e] == true) level++;
        }
        for(int i=0; i<=level+1; i++)
        {
            abilities[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateElementTexts();
    }

    void UpdateElementTexts()
    {
        waterText.text = waterNumber.ToString();
        fireText.text = fireNumber.ToString();
        airText.text = airNumber.ToString();
        earthText.text = earthNumber.ToString();
    }

    public void AddElementsFromCard(Card card)
    {
        switch (card.myCardFace.elementType)
        {
            case ElementType.Water:
                waterNumber += card.myCardFace.elementNumberGiven;
                break;
            case ElementType.Fire:
                fireNumber += card.myCardFace.elementNumberGiven;
                break;
            case ElementType.Air:
                airNumber += card.myCardFace.elementNumberGiven;
                break;
            case ElementType.Earth:
                earthNumber += card.myCardFace.elementNumberGiven;
                break;
            default:
                break;
        }
    }

    public void RemoveElementsFromCard(Card card)
    {
        switch (card.myCardFace.elementType)
        {
            case ElementType.Water:
                waterNumber -= card.myCardFace.elementNumberGiven;
                break;
            case ElementType.Fire:
                fireNumber -= card.myCardFace.elementNumberGiven;
                break;
            case ElementType.Air:
                airNumber -= card.myCardFace.elementNumberGiven;
                break;
            case ElementType.Earth:
                earthNumber -= card.myCardFace.elementNumberGiven;
                break;
            default:
                break;
        }
    }


}
