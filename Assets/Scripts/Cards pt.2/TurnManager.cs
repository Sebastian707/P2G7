using UnityEngine;

public enum TurnState { PlayerTurn, EnemyTurn }

public class TurnManager : MonoBehaviour
{
    public TurnState state;
    public EnemyAI enemy; // Reference to enemy
    public Player player; // Reference to player

    void Start()
    {
        state = TurnState.PlayerTurn;
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        Debug.Log("Player's Turn: Play a card.");
    }

    public void EndPlayerTurn()
    {
        state = TurnState.EnemyTurn;
        StartEnemyTurn();
    }

    void StartEnemyTurn()
    {
        Debug.Log("Enemy's Turn");
        enemy.TakeTurn(); // Call AI action

        Invoke(nameof(EndEnemyTurn), 2f); // Wait 2 sec before switching
    }

    void EndEnemyTurn()
    {
        state = TurnState.PlayerTurn;
        StartPlayerTurn();
    }
}