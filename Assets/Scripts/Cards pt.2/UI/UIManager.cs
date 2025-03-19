using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum TurnState { PlayerTurn, EnemyTurn }
    public Text energyText;
    public Text turnIndicator;

    public void UpdateEnergy(int current, int max)
    {
        energyText.text = $"Energy: {current}/{max}";
    }

    public void UpdateTurnIndicator(TurnState state)
    {
        turnIndicator.text = state == TurnState.PlayerTurn ? "Your Turn" : "Enemy Turn";
    }
}
