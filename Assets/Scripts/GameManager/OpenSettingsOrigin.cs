using UnityEngine;

public class OpenSettingsOrigin : MonoBehaviour
{
    // Verify how button was use to go to the setting (from main menu or from pause menu)
    public static bool fromMainMenu = false;

    // Stock the origin number level to return after settings
    public static string originLevelName = "";

    public void OpenSettings()
    {
        fromMainMenu = true;
    }
}
