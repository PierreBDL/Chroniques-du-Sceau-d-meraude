using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 3f;

    public Rigidbody2D rigidbody;

    public Animator animator;

    public SpriteRenderer spriteRenderer;

    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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

    // Repeated at fixed intervals for physics calculations
    void FixedUpdate()
    {
        rigidbody.linearVelocity = movement * moveSpeed;
    }
}
