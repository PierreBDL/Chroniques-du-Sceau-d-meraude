using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    // List of object that we don't destroy on load
    public GameObject[] objects;

    void Awake()
    {
        foreach (var element in objects)
        {
            // Check if there is another Player in the scene
            if (element.CompareTag("Player"))
            {
                GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
                
                // Destroy all existing Players except the one in this list
                foreach (GameObject existingPlayer in allPlayers)
                {
                    if (existingPlayer != element && existingPlayer != null)
                    {
                        // Destroy the existing Player from previous scene
                        Destroy(existingPlayer);
                    }
                }
            }

            DontDestroyOnLoad(element);
        }
    }
}
