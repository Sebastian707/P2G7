using UnityEngine;
using DialogueEditor;

public class NPCDialogue : MonoBehaviour
{

    public NPCConversation myConversation;
    public float conversationDistance = 3f;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearby())
        {
            ConversationManager.Instance.StartConversation(myConversation);
            DisableMovementOfCurrentCharacter();
        }

    }

    private bool IsPlayerNearby()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            return distance <= conversationDistance;
        }
        return false;
    }
    private void DisableMovementOfCurrentCharacter()
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false; // Disable movement
        }
    }

}

