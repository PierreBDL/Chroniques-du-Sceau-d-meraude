using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 3f;

    public Rigidbody2D rigidbody;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    private Vector2 movement;

    // Call reference to PlayerHealth script
    public Player_Health playerHealth;

    // Call pause menu script
    public PauseMenu pauseMenu;

    // Call InputManager to get custom key mappings
    public InputManager inputManager;

    // Update is called once per frame
    void Update()
    {
        // If the player is alive and the game is not paused, allow movement
        if (playerHealth.isAlive && (pauseMenu == null || !pauseMenu.isPaused))
        {
            // Reset movement
            movement = Vector2.zero;

            // Check custom key bindings from InputManager
            if (inputManager != null)
            {
                // Up
                if (Input.GetKey(inputManager.upKeyCode))
                {
                    movement.y = 1;
                }
                // Down
                if (Input.GetKey(inputManager.downKeyCode))
                {
                    movement.y = -1;
                }
                // Left
                if (Input.GetKey(inputManager.leftKeyCode))
                {
                    movement.x = -1;
                }
                // Right
                if (Input.GetKey(inputManager.rightKeyCode))
                {
                    movement.x = 1;
                }
            }
            else
            {
                // Fallback to default input if InputManager is not assigned
                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
            }

            // Normalize diagonal movement
            movement.Normalize();

            // Turn the player to face left or right
            if (movement.x != 0)
            {
                spriteRenderer.flipX = movement.x < 0;
            }

            // Squared magnitude -> transform vector 2 to float
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    // Repeated at fixed intervals for physics calculations
    void FixedUpdate()
    {
        rigidbody.linearVelocity = movement * moveSpeed;
    }
}
