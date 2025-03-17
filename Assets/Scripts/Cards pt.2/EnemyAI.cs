using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public int health = 50;
    public int attackPower = 10;

    public void TakeTurn()
    {
        int action = Random.Range(0, 2);
        if (action == 0)
            AttackPlayer();
        else
            Defend();
    }

    void AttackPlayer()
    {
        // Implement attack logic
    }

    void Defend()
    {
        // Implement defense logic
    }
}
