using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this instance between scenes
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
}