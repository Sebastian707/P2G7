using UnityEngine;

public class DrawEffect : CardEffect
{
    public int drawnCards = 5;
    public override void Use(CardPlayerScript cardPlayer)
    {
        cardPlayer.DrawCard(drawnCards);
       
    }
}
