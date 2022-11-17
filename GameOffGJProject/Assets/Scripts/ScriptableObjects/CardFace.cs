using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType { Water, Fire, Air, Earth }

[CreateAssetMenu(fileName = "NewCardFace", menuName = "Card Face")]
public class CardFace : ScriptableObject
{
    public string cardName;
    public string cardDescription;
    public ElementType elementType;
    public int elementNumberGiven;
    public Sprite detail1, cardFace;

}
