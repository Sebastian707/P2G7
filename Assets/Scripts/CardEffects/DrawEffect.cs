using UnityEngine;
public class DrawEffect : CardEffect
{
    public int drawnCards = 5;
    public override void Use()
    {
        CardPlayerScript.instance.DrawCard(drawnCards);

    }
    public override void Use(ICardPlayer owner, ICardPlayer target)
    {
        owner.DrawCard(drawnCards);
    }
}
