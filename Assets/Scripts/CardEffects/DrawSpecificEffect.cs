using UnityEngine;

public class DrawSpecificEffect : CardEffect
{
    [SerializeField] private int amount;
    [SerializeField] private GameObject drawnCard;
    public override void Use()
    {
        CardPlayerScript.instance.DrawCard(amount,drawnCard);

    }
}
