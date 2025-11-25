using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseSettingsScript : MonoBehaviour
{
    public void CloseSettings()
    {
        // If settings was opened from main menu, return to main menu
        if (OpenSettingsOrigin.fromMainMenu == true)
        {
            OpenSettingsOrigin.fromMainMenu = false;
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            return;
        } else if (OpenSettingsOrigin.originLevelName != ""){
            // If settings was opened from pause menu, return to the origin level
            UnityEngine.SceneManagement.SceneManager.LoadScene(OpenSettingsOrigin.originLevelName);
        } else {
            // Default return to Level01
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level01");
        }
    }
}
