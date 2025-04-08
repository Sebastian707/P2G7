using UnityEngine;
using Unity.Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public string zoneName;
    public CinemachineCamera roomCamera;

    [System.Obsolete]
    private void Start()
    {
        CameraManager cameraManager = FindObjectOfType<CameraManager>();
        if (cameraManager != null)
        {
            cameraManager.RegisterCamera(zoneName, roomCamera);
        }
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager cameraManager = FindObjectOfType<CameraManager>();
            if (cameraManager != null)
            {
                cameraManager.SwitchCamera(zoneName);
            }
        }
    }

    [System.Obsolete]
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager cameraManager = FindObjectOfType<CameraManager>();
            if (cameraManager != null)
            {
                cameraManager.ResetToDefault();
            }
        }
    }
}
