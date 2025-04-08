using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    public Text questStatusText; 
    public List<Quest> activeQuests = new List<Quest>(); 
    public List<Quest> completedQuests = new List<Quest>();  
    public List<string> questLogs = new List<string>(); 


    public void StartQuest(Quest quest)
    {
        if (quest == null) return;

        activeQuests.Add(quest);
        questLogs.Add("Started quest: " + quest.questTitle); 


        Debug.Log("Quest started: " + quest.questTitle);

        if (questStatusText != null)
        {
            questStatusText.text = "Quest Started: " + quest.questTitle;
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (quest == null) return;

        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            questLogs.Add("Completed quest: " + quest.questTitle); 
        }
    }
}
