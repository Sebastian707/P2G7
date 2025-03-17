using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Card2;

public class CardEffect : MonoBehaviour
{
    public interface ICardEffect
{
    void ApplyEffect(Character target);
}

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public int cost;
    public CardType type;
    public Sprite artwork;
    public ICardEffect effect; // Reference to effect

    public void PlayCard(Character target)
    {
        effect?.ApplyEffect(target);
    }
}

public class DamageEffect : ICardEffect
{
    public int damageAmount;

    public void ApplyEffect(Character target)
    {
        target.TakeDamage(damageAmount);
    }
}
}
