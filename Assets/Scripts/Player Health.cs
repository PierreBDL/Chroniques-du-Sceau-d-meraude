using UnityEngine;
using System.Collections.Generic;

public class Player_Health : MonoBehaviour
{
    // Health
    public int maxHealth = 3;
    private int currentHealth;

    // Die or not
    public bool isAlive = true;

    // Healthbar UI
    public Transform healthbarUI;

    // Prefabs of the health
    public GameObject hpPrefab;

    // List to keep track of health icons
    private List<GameObject> healthIcons = new List<GameObject>();

    // Animator
    public Animator animator;

    // Sprite Renderer
    public SpriteRenderer spriteRenderer;

    // Reset health
    void Start () 
    {
        currentHealth = maxHealth;

        // Update healthbar
        UpdateHeathbarUI();
    }

    // Damage managment
    public void TakeDamage (int damage)
    {
        // if player is alive -> can take damage
        if (isAlive) {
            currentHealth -= damage;

            // Forbid negative health
            if (currentHealth < 0)
                currentHealth = 0;

            // Update healthbar
            UpdateHeathbarUI();

            // Check if player is dead
            if (currentHealth <= 0)
            {
                animator.SetTrigger("Die");
            
                // Player is dead
                isAlive = false;
            }
        }
    }

    // Upgrade healthbar
    public void UpdateHeathbarUI ()
    {
        // Delete all healths
        foreach (GameObject icon in healthIcons)
        {
            // Check if icon is not null to avoid errors
            if (icon != null)
                Destroy(icon);
        }
        // Clear the list after destroying all icons
        healthIcons.Clear();

        // Create healths from prefab and add them to the list
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject newHP = Instantiate(hpPrefab, healthbarUI);
            healthIcons.Add(newHP);
        }
    }

    // Diseable player if is dead
    public void DisablePlayerVisual ()
    {
        spriteRenderer.enabled = false;
    }
}
