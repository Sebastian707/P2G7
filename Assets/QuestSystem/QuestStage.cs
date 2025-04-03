using UnityEngine;

[System.Serializable]
public class QuestStage
{
    public string stageName;
    public string stageDescription;
    public QuestObjective[] objectives; 
    public bool isComplete;

    public bool IsComplete()
    {
        foreach (var objective in objectives)
        {
            if (!objective.IsComplete()) 
                return false;
        }
        return true;
    }
}
