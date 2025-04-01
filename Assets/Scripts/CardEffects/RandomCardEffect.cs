using UnityEngine;

public class RandomCardEffect : CardEffect
{
    [SerializeField] GameObject[] cards;
    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    public override void Use(ICardPlayer owner, ICardPlayer target)
    {
        GameObject usedCard = cards[Random.Range(0, cards.Length)];
        Instantiate(usedCard).GetComponent<Card>().UseCard(owner,target);
    }
}
