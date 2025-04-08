using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class CameraManager : MonoBehaviour
{
    public CinemachineCamera defaultCamera;
    private Dictionary<string, CinemachineCamera> cameraZones = new Dictionary<string, CinemachineCamera>();

    public CinemachineCamera activeCamera;

    [System.Obsolete]
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    [System.Obsolete]
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    [System.Obsolete]
    void Start()
    {
        RegisterAllCamerasInScene();

        if (defaultCamera != null)
        {
            activeCamera = defaultCamera;
            defaultCamera.Priority = 10;
        }
    }

    [System.Obsolete]
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterAllCamerasInScene(true);
    }

    public void RegisterCamera(string zoneName, CinemachineCamera camera)
    {
        if (!cameraZones.ContainsKey(zoneName))
        {
            cameraZones.Add(zoneName, camera);
        }
    }

    public void SwitchCamera(string zoneName)
    {
        if (cameraZones.TryGetValue(zoneName, out CinemachineCamera newCamera))
        {
            if (activeCamera != newCamera)
            {
                activeCamera.Priority = 5;
                newCamera.Priority = 10;    
                activeCamera = newCamera;
            }
        }
        else
        {
            ResetToDefault();
        }
    }

    public void ResetToDefault()
    {
        if (activeCamera != defaultCamera)
        {
            activeCamera.Priority = 5;
            defaultCamera.Priority = 10;
            activeCamera = defaultCamera;
        }
    }

    [System.Obsolete]
    public void RegisterAllCamerasInScene(bool clearExisting = false)
    {
        if (clearExisting)
        {
            cameraZones.Clear();
        }

        var camerasInScene = FindObjectsOfType<CinemachineCamera>();
        foreach (var camera in camerasInScene)
        {
            CameraTrigger cameraTrigger = camera.GetComponent<CameraTrigger>();
            if (cameraTrigger != null)
            {
                RegisterCamera(cameraTrigger.zoneName, camera);
            }
        }
    }
}
