using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZoneManager : MonoBehaviour
{
    public List<Zone> zones; // Assign in Inspector
    private string currentZone;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newZone = GetZoneForScene(scene.name);

        if (newZone != currentZone)
        {
            currentZone = newZone;
            ShowZoneMessage(newZone);
        }
    }

    string GetZoneForScene(string sceneName)
    {
        foreach (var zone in zones)
        {
            if (zone.sceneNames.Contains(sceneName))
            {
                return zone.zoneName;
            }
        }
        return "Unknown Zone";
    }

    void ShowZoneMessage(string zoneName)
    {
        // Show your UI message here
        Debug.Log("Entered Zone: " + zoneName);
    }
}
