using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    void Awake()
    {
        // Set the player's position to this spawn point's position
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
