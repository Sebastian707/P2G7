public class QuestProgress
{
    public Quest quest;
    public int currentStageIndex;

    public QuestProgress(Quest quest)
    {
        this.quest = quest;
        currentStageIndex = 0;
    }

    public void UpdateObjectiveProgress(QuestObjective objective)
    {
        objective.isComplete = objective.IsComplete();  
    }

    public bool IsStageComplete()
    {
        return quest.stages[currentStageIndex].IsComplete();
    }

    public void CompleteStage()
    {
        if (currentStageIndex < quest.stages.Length - 1)
        {
            currentStageIndex++;
        }
    }

    public bool IsQuestComplete()
    {
        return currentStageIndex >= quest.stages.Length;
    }
}
