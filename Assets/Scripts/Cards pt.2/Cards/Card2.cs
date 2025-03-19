using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card2 : ScriptableObject
{
    public string cardName;
    public int cost;
    public CardType type;
    public Sprite artwork;
    public CardEffect effect;

    public void PlayCard(Character target)
    {
        if (effect != null)
        {
            effect.ApplyEffect(target);
            Debug.Log(cardName + " was played!");
        }
        else
        {
            Debug.Log("No effect assigned to this card.");
        }
    }
}
