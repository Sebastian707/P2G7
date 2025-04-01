using UnityEngine;

public class ModifyHealthEffect : CardEffect
{
 [SerializeField] bool targetSelf;
    [SerializeField] int healthChange;
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
    public override void Use(ICardPlayer owner, ICardPlayer target)
    {
        if (targetSelf) target = owner;
        target.Health += healthChange;
    }
}
