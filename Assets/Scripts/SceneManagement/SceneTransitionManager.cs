using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    public CanvasGroup fadeCanvasGroup; 
    public float fadeDuration = 1f;

    private string lastExitName;
    [SerializeField] private float fadeLinger = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [System.Obsolete]
    public void TransitionToScene(string sceneName, string exitName)
    {
        lastExitName = exitName;
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    [System.Obsolete]
    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        yield return StartCoroutine(Fade(1));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        while (!operation.isDone)
        {
            yield return null;
        }

        PlayerSpawnPoint spawnPoint = FindSpawnPoint();
        if (spawnPoint != null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = spawnPoint.transform.position;
                player.transform.rotation = spawnPoint.transform.rotation;
            }
        }
        yield return new WaitForSeconds(fadeLinger);

        yield return StartCoroutine(Fade(0));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }

    [System.Obsolete]
    private PlayerSpawnPoint FindSpawnPoint()
    {
        PlayerSpawnPoint[] spawnPoints = FindObjectsOfType<PlayerSpawnPoint>();
        foreach (var spawn in spawnPoints)
        {
            if (spawn.spawnPointName == lastExitName)
            {
                return spawn;
            }
        }
        return null;
    }
}