using UnityEngine;

public class Card2 : MonoBehaviour
{
    public enum CardType { Attack, Defense, Buff, Debuff, Special }

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public CardType type;
    public int cost;
    public Sprite artwork;
}

}
