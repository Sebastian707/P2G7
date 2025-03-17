using UnityEngine;

[CreateAssetMenu(fileName = "New Damage Effect", menuName = "Card Effects/Damage")]
public class DamageEffect : ScriptableObject, ICardEffect
{
    public int damageAmount;

    public void ApplyEffect(Character target)
    {
        if (target != null)
        {
            target.TakeDamage(damageAmount);
            Debug.Log(target.characterName + " takes " + damageAmount + " damage!");
        }
    }
}
