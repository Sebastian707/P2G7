using UnityEngine;

public class EnemyAI : Character
{
    public int attackPower = 10;
    private Player player; // Reference to player

    void Start()
    {
        // Find player in the scene
        player = FindAnyObjectByType<Player>();
    }

    public void TakeTurn()
    {
        if (player == null) return; // If no player, skip turn

        int action = Random.Range(0, 2);
        if (action == 0)
            AttackPlayer();
        else
            Defend();
    }

    void AttackPlayer()
    {
        Debug.Log(name + " attacks the player!");
        player.TakeDamage(attackPower);
    }

    void Defend()
    {
        Debug.Log(name + " defends and gains armor!");
        // Example: Gain temporary armor (modify this if needed)
        health += 5;
    }
}