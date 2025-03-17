using UnityEngine;

public class Player : Character
{
    public int energy = 3;

    public void PlayCard(Card card, EnemyAI target)
{
    if (energy >= card.cost)
    {
        Debug.Log("Player played: " + card.cardName);
        card.PlayCard(target);
        energy -= card.cost;
    }
    else
    {
        Debug.Log("Not enough energy!");
    }
}
}
