using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public GameObject pauseMenuUI;

    // Disable the pause menu at the start of the game
    void Awake()
    {
        pauseMenuUI.SetActive(false);
    }

    void Start() {
        isPaused = false;
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If the game is paused, resume it
            if (isPaused)
            {
                Resume();
            }
            else
            {
                // If the game is not paused, pause it
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Settings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    // Main Menu button
    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenuUI.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
