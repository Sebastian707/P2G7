using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth = 50;
    public int health;

    void Start()
    {
        health = maxHealth; // Initialize health
    }

public void Heal(int amount)
{
    health = Mathf.Min(health + amount, maxHealth);
    Debug.Log(characterName + " heals for " + amount + " HP!");
}

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(characterName + " takes " + damage + " damage!");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(characterName + " has been defeated.");
        Destroy(gameObject); // Remove from game
    }
}