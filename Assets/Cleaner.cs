using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Cleaner : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(CleanupAndLoadMainMenu());
    }

    private IEnumerator CleanupAndLoadMainMenu()
    {
        // Get all root objects in DontDestroyOnLoad
        GameObject[] allObjects = FindAllDontDestroyOnLoadObjects();

        foreach (var obj in allObjects)
        {
            Destroy(obj);
        }

        // Wait a frame to ensure destruction
        yield return null;

        yield return new WaitForSeconds(5f);
        // Now load the main menu
        SceneManager.LoadScene("MainMenuScene");
    }

    private GameObject[] FindAllDontDestroyOnLoadObjects()
    {
        var temp = new GameObject("TempSceneRoot");
        DontDestroyOnLoad(temp);

        Scene dontDestroyOnLoadScene = temp.scene;
        DestroyImmediate(temp);

        var rootObjects = new List<GameObject>();
        foreach (GameObject go in dontDestroyOnLoadScene.GetRootGameObjects())
        {
            if (go != gameObject) // Don't destroy the Cleaner itself
            {
                rootObjects.Add(go);
            }
        }
        return rootObjects.ToArray();
    }
}

