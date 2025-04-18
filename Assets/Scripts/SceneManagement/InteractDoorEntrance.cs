using UnityEngine.SceneManagement;
using UnityEngine;

public class InteractDoorEntrance : MonoBehaviour
{
    private GameObject player;
    public float InteractDistance = 3f;
    [SerializeField] private GameObject InteractIndicaton;
    [SerializeField] private Transform InteractPosition;
    private GameObject currentInteractIndicator;
    public string targetScene;
    public string entranceName;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {

        if (IsPlayerNearby())
        {
            if (currentInteractIndicator == null)
            {
                SpeechIndicator();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Door opened");
                SceneTransitionManager.Instance.TransitionToScene(targetScene, entranceName);
            }
        }
        else
        {
            if (currentInteractIndicator != null)
            {
                Destroy(currentInteractIndicator);
                currentInteractIndicator = null;
            }
        }
    }

    private bool IsPlayerNearby()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            return distance <= InteractDistance;
        }
        return false;
    }
    private void SpeechIndicator()
    {
        currentInteractIndicator = Instantiate(InteractIndicaton, InteractPosition.position, Quaternion.identity);
        currentInteractIndicator.transform.SetParent(this.transform);

    }
}
