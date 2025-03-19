public class DrawEffect : CardEffect
{
    public int drawnCards = 5;
    public override void Use()
    {
        CardPlayerScript.instance.DrawCard(drawnCards);

    }
}
