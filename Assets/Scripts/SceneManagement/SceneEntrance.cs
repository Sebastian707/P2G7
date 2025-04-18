using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string targetScene;      
    public string entranceName;

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneTransitionManager.Instance.TransitionToScene(targetScene, entranceName);
        }
    }
}

