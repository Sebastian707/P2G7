using UnityEngine;
using DialogueEditor;

public class NPCDialogue : MonoBehaviour
{
    public NPCConversation myConversation;
    public float conversationDistance = 3f;
    [SerializeField] private GameObject SpeechIndicaton;
    [SerializeField] private Transform SpeechPosition;
    private GameObject currentSpeechIndicator;

    private GameObject player;

    private void Awake()
    {
        if (ConversationManager.Instance == null)
        {
            ConversationManager found = FindFirstObjectByType<ConversationManager>();
            if (found != null)
            {
                typeof(ConversationManager)
                    .GetProperty("Instance")
                    ?.SetValue(null, found);
            }
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {

        if (IsPlayerNearby() && !ConversationManager.Instance.IsConversationActive)
        {
            if (currentSpeechIndicator == null)
            {
                SpeechIndicator();
            }
        }
        else
        {
            if (currentSpeechIndicator != null)
            {
                Destroy(currentSpeechIndicator);
                currentSpeechIndicator = null;
            }
        }

        // Start conversation on key press
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearby() && !ConversationManager.Instance.IsConversationActive)
        {
            ConversationManager.Instance.StartConversation(myConversation);
            DisableMovementOfCurrentCharacter();

            if (currentSpeechIndicator != null)
            {
                Destroy(currentSpeechIndicator);
                currentSpeechIndicator = null;
            }
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

    private void SpeechIndicator()
    {
        currentSpeechIndicator = Instantiate(SpeechIndicaton, SpeechPosition.position, Quaternion.identity);
        currentSpeechIndicator.transform.SetParent(this.transform);
    }
}

