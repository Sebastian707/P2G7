using UnityEngine;

public abstract class QuestObjective : ScriptableObject
{
    public string description;
    public bool isComplete;

    public abstract bool IsComplete(); 
}
