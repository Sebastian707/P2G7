using UnityEngine;
using DialogueEditor;

public class DialogueEventTrigger : MonoBehaviour
{
    public string characterID;
    public NPCDialogue npcDialogue;
    public NPCConversation[] possibleConversations;

    private void Start() // Moved from Awake() to Start()
    {
        if (string.IsNullOrEmpty(characterID))
        {
            characterID = gameObject.name; // fallback to name if ID isn't manually assigned
        }

        // Try to restore conversation state
        if (DialogueManager.Instance != null)
        {
            if (DialogueManager.Instance.TryGetSavedConversation(characterID, out var savedConversation))
            {
                npcDialogue.myConversation = savedConversation;
            }
            else
            {
                DialogueManager.Instance.RegisterDialogueState(characterID, npcDialogue.myConversation);
            }
        }
        else
        {
            Debug.LogWarning("DialogueManager.Instance is not initialized yet.");
        }
    }

    public void ChangeConversation(int conversationIndex)
    {
        if (npcDialogue != null && conversationIndex >= 0 && conversationIndex < possibleConversations.Length)
        {
            var selected = possibleConversations[conversationIndex];
            npcDialogue.myConversation = selected;
            DialogueManager.Instance.RegisterDialogueState(characterID, selected);
            Debug.Log($"{characterID} switched to conversation: {selected.name}");
        }
    }
}