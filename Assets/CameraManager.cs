using UnityEngine;
using Unity.Cinemachine;

public class CameraDampingController : MonoBehaviour
{
    [Header("References")]
    public CinemachineCamera cinemachineCamera; 
    public Rigidbody2D playerRb; 

    [Header("Damping Settings")]
    public float defaultYDamping = 1.0f;  
    public float fallingYDamping = 0.25f;
    public float fallThreshold = -2f; 
    public float dampingTransitionSpeed = 3f; 

    private CinemachinePositionComposer positionComposer;
    private float currentDamping; 

    void Start()
    {
        if (cinemachineCamera != null)
        {
            positionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
            if (positionComposer != null)
            {
                currentDamping = defaultYDamping = positionComposer.Damping.y; // Store initial damping
            }
        }
    }

    void Update()
    {
        if (cinemachineCamera == null || playerRb == null) return;

        positionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
        if (positionComposer == null) return;

        float targetDamping = (playerRb.linearVelocity.y < fallThreshold) ? fallingYDamping : defaultYDamping;

        currentDamping = Mathf.Lerp(currentDamping, targetDamping, Time.deltaTime * dampingTransitionSpeed);

        positionComposer.Damping.y = currentDamping;
    }
}

