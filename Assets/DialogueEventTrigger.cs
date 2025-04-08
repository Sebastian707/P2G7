using System.Collections;
using UnityEngine;
using DialogueEditor;

public class DialogueEventTrigger : MonoBehaviour
{
    public string characterID;
    public NPCDialogue npcDialogue;
    public NPCConversation[] possibleConversations;

    private IEnumerator Start()
    {
        yield return null;

        if (string.IsNullOrEmpty(characterID))
        {
            characterID = gameObject.name;
        }

        if (DialogueManager.Instance != null)
        {
            if (DialogueManager.Instance.TryGetSavedConversation(characterID, possibleConversations, out var savedConversation))
            {
                npcDialogue.myConversation = savedConversation;
                Debug.Log($"[DialogueEventTrigger] {characterID} loaded saved conversation: {savedConversation.name}");
            }
            else
            {
                DialogueManager.Instance.RegisterDialogueState(characterID, npcDialogue.myConversation);
                Debug.Log($"[DialogueEventTrigger] {characterID} registered initial conversation: {npcDialogue.myConversation.name}");
            }
        }
    }

    public void ChangeConversation(int conversationIndex)
    {
        if (npcDialogue != null && conversationIndex >= 0 && conversationIndex < possibleConversations.Length)
        {
            var selected = possibleConversations[conversationIndex];
            npcDialogue.myConversation = selected;
            DialogueManager.Instance.RegisterDialogueState(characterID, selected);
            Debug.Log($"[DialogueEventTrigger] {characterID} switched to conversation: {selected.name}");
        }
    }
}
