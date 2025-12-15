using UnityEngine;
using System.Collections.Generic;

public class Player_Health : MonoBehaviour
{
    // Health
    private static int maxHealth = 3;
    private static int currentHealth;
    public int maxHealthWithUpgrades = 7;

    // Die or not
    public bool isAlive = true;

    // Healthbar UI
    public Transform healthbarUI;

    // Prefabs of the health
    public GameObject hpPrefab;

    // List to keep track of health icons
    private List<GameObject> healthIcons = new List<GameObject>();

    // Player
    public GameObject player;

    // Initialize health
    private static bool isInitialized = false;

    // Bool all components are found
    private bool componentsFound = false;

    // First initialization of health
    void Awake ()
    {
        if (!isInitialized)
        {
            currentHealth = maxHealth;
            isInitialized = true;
        }
    }

    // Reset health
    void Start () 
    {
        if (player == null || healthbarUI == null)
        {
            componentsFound = false;
        }

        // Find healthbar UI if not assigned
        if (healthbarUI == null)
        {
            // Search components by tag
            GameObject healthbarObject = GameObject.FindGameObjectWithTag("HealthBar");
        
            if (healthbarObject != null)
            {
                healthbarUI = healthbarObject.transform;
            }
            else
            {
                // Else search by name
                healthbarObject = GameObject.Find("Healthbar");
                
                if (healthbarObject != null)
                {
                    healthbarUI = healthbarObject.transform;
                }
            }
        }

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (player != null && healthbarUI != null)
        {
            componentsFound = true;
            // Update healthbar
            UpdateHeathbarUI();
        }   
    }

    void Update () 
    {

        if (player == null || healthbarUI == null)
        {
            componentsFound = false;
        }


        // Find player if not assigned and update healthbar
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (healthbarUI == null)
        {
            // Search components by tag
            GameObject healthbarObject = GameObject.FindGameObjectWithTag("HealthBar");
        
            if (healthbarObject != null)
            {
                healthbarUI = healthbarObject.transform;
            }
            else
            {
                // Else search by name
                healthbarObject = GameObject.Find("Healthbar");
                
                if (healthbarObject != null)
                {
                    healthbarUI = healthbarObject.transform;
                }
            }
        }

        if (player != null && healthbarUI != null && !componentsFound)
        {
            componentsFound = true;
            // Update healthbar
            UpdateHeathbarUI();
        }
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
                player.GetComponent<Animator>().SetTrigger("Die");
            
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
        player.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Regenerate health
    public void RegenerateHealth (int healthRestore)
    {
        // Only if player is alive
        if (isAlive) {
            currentHealth += healthRestore;

            // Forbid health overflow
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            // Update healthbar
            UpdateHeathbarUI();
        }
    }

    // Upgrade max health
    public void UpgradeMaxHealth (int healthUpgrade) {
        if (isAlive) {
            if (maxHealth + healthUpgrade <= maxHealthWithUpgrades) {
                maxHealth += healthUpgrade;
            } else {
                maxHealth = maxHealthWithUpgrades;
            }
            currentHealth = maxHealth;

            // Update healthbar
            UpdateHeathbarUI();
        }
    }
}
