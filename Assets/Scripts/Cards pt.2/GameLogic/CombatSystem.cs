using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    public enum TurnState { PlayerTurn, EnemyTurn }

public class TurnManager : MonoBehaviour
{
    public TurnState state;

    void Start()
    {
        state = TurnState.PlayerTurn;
        StartPlayerTurn();
    }

    void StartPlayerTurn()
    {
        // Allow card play
    }

    void EndPlayerTurn()
    {
        state = TurnState.EnemyTurn;
        StartEnemyTurn();
    }

    void StartEnemyTurn()
    {
        // Enemy takes action
        Invoke(nameof(EndEnemyTurn), 2f);
    }

    void EndEnemyTurn()
    {
        state = TurnState.PlayerTurn;
        StartPlayerTurn();
    }
}
}
