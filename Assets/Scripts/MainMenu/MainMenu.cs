using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Call OriginOpenSettings script to set fromMainMenu to true when opening settings from main menu
    public OpenSettingsOrigin OpenSettingsOriginScript;

    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level01");
    }

    public void Settings()
    {
        OpenSettingsOriginScript.OpenSettings();
        UnityEngine.SceneManagement.SceneManager.LoadScene("Settings");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
