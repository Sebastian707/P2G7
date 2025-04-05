using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    private Dictionary<string, NPCConversation> dialogueStates = new Dictionary<string, NPCConversation>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterDialogueState(string characterID, NPCConversation currentConversation)
    {
        dialogueStates[characterID] = currentConversation;
    }

    public bool TryGetSavedConversation(string characterID, out NPCConversation conversation)
    {
        return dialogueStates.TryGetValue(characterID, out conversation);
    }
}
