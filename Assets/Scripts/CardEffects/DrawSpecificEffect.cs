using UnityEngine;

public class DrawSpecificEffect : CardEffect
{
    [SerializeField] private int amount;
    [SerializeField] private GameObject drawnCard;
    [SerializeField] GameObject[] cards;
    public override void Use()
    {
        CardPlayerScript.instance.DrawCard(amount, drawnCard);

    }

    public override void Use(ICardPlayer owner, ICardPlayer target)
    {
        foreach(GameObject card in cards)
        {
            owner.DrawCard(1, card);
        }
    }
}
