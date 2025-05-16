using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneResetter : MonoBehaviour
{
    public void ResetToMainMenu()
    {
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        // Load the cleaner scene first
        SceneManager.LoadScene("CleanerScene");

        // Wait a frame to let CleanerScene initialize
        yield return null;
    }
}
