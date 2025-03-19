using UnityEngine;

public class DamageEffect : CardEffect
{
    [SerializeField] private int damageAmount = 3;
    public override void Use()
    {
        CardGameMaster.instance.EnemyHealth -= damageAmount;
        
    }
}
