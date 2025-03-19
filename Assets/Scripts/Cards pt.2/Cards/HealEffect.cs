using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Effect", menuName = "Card Effects/Heal")]
public class HealEffect : ScriptableObject, ICardEffect
{
    public int healAmount;

    public void ApplyEffect(Character target)
    {
        if (target != null)
        {
            target.Heal(healAmount);
            Debug.Log(target.characterName + " heals for " + healAmount + " HP!");
        }
    }
}
