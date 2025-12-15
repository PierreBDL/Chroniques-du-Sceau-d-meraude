using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1.5f;

    public int damage = 1;

    // Knockback force
    public float knockbackForce = 5f;

    public SpriteRenderer spriteRenderer;

    public Animator animator;

    // Call PlayerHealth script
    public Player_Health playerHealth;

    // Call EnemyHealth script
    public EnemyAI enemyScript;

    // Call pause menu script
    public PauseMenu pauseMenu;

    // Call keyManagement script
    public InputManager inputManager;

    void Start()
    {
        // Find Player_Health component if not assigned
        if (playerHealth == null)
        {
            playerHealth = GameObject.Find("PlayerStats").GetComponent<Player_Health>();
            if (playerHealth == null)
            {
                Debug.LogError("Player_Health component not found on the player!");
            }
        }

        // Find PauseMenu component if not assigned
        if (pauseMenu == null)
        {
            GameObject pauseMenuObject = GameObject.FindWithTag("PauseMenu");
            if (pauseMenuObject != null)
            {
                pauseMenu = pauseMenuObject.GetComponent<PauseMenu>();
            }
        }

        // Find InputManager component if not assigned
        if (inputManager == null)
        {
            inputManager = GameObject.FindWithTag("InputManager").GetComponent<InputManager>();
        }
    }

    void Update()
    {
         if (inputManager != null)
        {
            // If the attack key is pressed and the player is alive and the game is not paused, perform an attack
            if (Input.GetKeyDown(inputManager.attackKeyCode) && playerHealth.isAlive && (pauseMenu == null || !pauseMenu.isPaused))
            {
                // Call the attack function
                PermformAttack();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && playerHealth.isAlive && (pauseMenu == null || !pauseMenu.isPaused))
            {
                // Call the attack function
                PermformAttack();
            }
        }   
    }

    void PermformAttack()
    {
        // Play attack animation
        animator.SetTrigger("Attack");

        // Look at the direction the player is facing
        Vector2 attackDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        // Take all colliders (ennemies) within the attack range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
    
        // Loop through all colliders found
        foreach (Collider2D collider in hitColliders)
        {
            // If the collider has the tag "Enemy"
            if (collider.CompareTag("Enemy"))
            {
                // Look if ennemy is in front of the player (positif x for right, negatif x for left)
                Vector2 directionToEnemy = (collider.transform.position - transform.position).normalized;

                // Check if the enemy is in the attack direction
                if (Vector2.Dot(attackDirection, directionToEnemy) > 0)
                {
                    // Reduce the enemy's health after calling the TakeDamage function from EnemyAI script
                    EnemyAI enemyScript = collider.GetComponent<EnemyAI>();
                    enemyScript.TakeDamage(damage);

                    // Knockback effect
                    Vector2 KnockbackDirection = (collider.transform.position - transform.position).normalized;
                    enemyScript.rb.AddForce(KnockbackDirection * knockbackForce, ForceMode2D.Impulse);
                }  
            }
        }
    }

    // Visualize the attack range in the editor
    void OnDrawGizmosSelected()
    {
        // Draw a red wire sphere to represent the attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
