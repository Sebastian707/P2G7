using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private string targetScene;
    [SerializeField] private string entranceName;
    public static PauseScript Instance;
    [SerializeField] private GameObject ConfirmMenu;
    [SerializeField] private GameObject PauseScreenMain;
    [SerializeField] private GameObject ControlsMenu;

    public GameObject pauseScreen;
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
        pauseScreen.SetActive(false);
        
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf)
            {
                Time.timeScale = 1;
                pauseScreen.SetActive(false);
            }
            else
            {
                Time.timeScale = 0;
                pauseScreen.SetActive(true);

                PauseScreenMain.SetActive(true);
                ControlsMenu.SetActive(false);
                ConfirmMenu.SetActive(false);
            }
        }
    }
    public void Unpause()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
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

    public void OnControls()
    {
        PauseScreenMain.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    public void OnReturn()
    {
        PauseScreenMain.SetActive(true);
        ControlsMenu.SetActive(false);
    }
}
