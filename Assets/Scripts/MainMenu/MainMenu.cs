using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject playMenu;
    [SerializeField] private GameObject CreditsMenu;
    [SerializeField] private GameObject ConfirmMenu;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private string targetScene;
    [SerializeField] private string entranceName;

    [System.Obsolete]
    public void OnBegin()
    {
        SceneTransitionManager.Instance.TransitionToScene(targetScene, entranceName);
    }

    public void OnCredits()
    {
        playMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void OnReturn()
    {
        playMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void OnQuit()
    {
        ConfirmMenu.SetActive(true);
    }


    public void OnConfirmQuit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnConfirmDeny()
    {
        ConfirmMenu.SetActive(false);
        
    }

    public void OnClick()
    {
        PlaySound(buttonClickSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
