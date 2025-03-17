using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card2 : ScriptableObject
{
    public string cardName;
    public string description;
    public CardType type;
    public int cost;
    public Sprite artwork;
    public ICardEffect effect;
    public enum CardType
{
    Attack,
    Defense,
    Buff,
    Debuff,
    Special
}

    public void PlayCard(Character target)
    {
        if (effect != null)
        {
            effect.ApplyEffect(target);
        }
    }
}
