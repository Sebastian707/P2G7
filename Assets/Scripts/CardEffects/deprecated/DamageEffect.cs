using UnityEngine;

public class DamageEffect : CardEffect
{
    [SerializeField] private int damageAmount = 3;
    public override void Use()
    {
        CardGameMaster.instance.EnemyHealth -= damageAmount;

    }

    public override void Use(ICardPlayer owner, ICardPlayer target)
    {
        Debug.LogWarning("card still uses old use method", this);
        Use();
    }
}
