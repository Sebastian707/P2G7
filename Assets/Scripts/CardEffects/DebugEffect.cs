using UnityEngine;

public class DebugEffect : CardEffect
{
    public string debugText = "no  debug text was added";
    public override void Use()
    {
        Debug.Log(debugText);
    }

    public override void Use(ICardPlayer owner, ICardPlayer target)
    {
        Debug.LogWarning("card still uses old use method", this);
        Use();
    }
}
