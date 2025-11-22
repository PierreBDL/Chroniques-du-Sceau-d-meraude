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
                GameObject existingPlayer = GameObject.FindGameObjectWithTag("Player");
                
                // Destroy the new Player
                if (existingPlayer != null && existingPlayer != element)
                {
                    // Destroy the new Player
                    Destroy(element);
                    continue;
                }
            }

            DontDestroyOnLoad(element);
        }
    }
}
