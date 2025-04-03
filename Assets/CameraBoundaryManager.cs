using UnityEngine;
using Unity.Cinemachine;

public class CameraBoundaryManager : MonoBehaviour
{
    private CinemachineConfiner2D confiner;

    [System.Obsolete]
    void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner2D>(); // Find CinemachineConfiner
        if (confiner)
        {
            AssignNewBoundary();
        }
        else
        {
            Debug.Log("No boundary");
        }
    }

    [System.Obsolete]
    void AssignNewBoundary()
    {
        GameObject boundaryObject = GameObject.FindWithTag("CameraBoundary"); // Ensure boundary is tagged properly

        if (boundaryObject != null)
        {
            CompositeCollider2D boundaryCollider = boundaryObject.GetComponent<CompositeCollider2D>();
            if (boundaryCollider != null)
            {
                confiner.BoundingShape2D = boundaryCollider;
                confiner.InvalidateCache();
            }
        }
    }

    [System.Obsolete]
    void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += (scene, mode) => AssignNewBoundary();
    }

    [System.Obsolete]
    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= (scene, mode) => AssignNewBoundary();
    }
}

