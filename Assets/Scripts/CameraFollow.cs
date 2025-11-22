using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Call of player object
    public Transform player;

    // Offset between camera and player (delay between movements)
    public float timeOffset = 0.2f;

    // Position offset of camera
    public Vector3 posOffset;

    // Velocity reference for SmoothDamp
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position + posOffset, ref velocity, timeOffset);
    }
}
