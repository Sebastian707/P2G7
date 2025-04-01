using UnityEngine;
public class EffectTemplate : CardEffect
{
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
    public override void Use(ICardPlayer owner, ICardPlayer target)
    {
        Debug.LogWarning("card still uses old use method", this);
        Use();
    }
}
