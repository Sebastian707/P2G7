using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public void EndEnemyTurn()
    {
        Debug.Log("GameMaster: Enemy turn ended.");
        // Your logic to switch back to player turn
    }

    public void EndPlayerTurn()
    {
        Debug.Log("GameMaster: Player turn ended.");
        // Call enemyAI.TakeTurn() here
    }
}
