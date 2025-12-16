using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    // Singleton instance
    public static Player_Health Instance;

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

    // UI initialized
    private bool uiInitialized = false;

    // First initialization of health
    void Awake()
    {
        // Singleton protection
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!isInitialized)
        {
            currentHealth = maxHealth;
            isInitialized = true;
        }
    }

    // Search ui every scene load
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called when a new scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset references (UI is destroyed/recreated)
        healthbarUI = null;
        uiInitialized = false;
    }

    void Update()
    {
        if (!uiInitialized)
        {
            GameObject hb = GameObject.FindGameObjectWithTag("HealthBar");
            if (hb != null)
            {
                healthbarUI = hb.transform;
                BuildHealthbar();
                uiInitialized = true;
            }
        }
    }

    // Damage managment
    public void TakeDamage(int damage)
    {
        if (!isAlive) return;

        currentHealth -= damage;
        if (currentHealth < 0)
            currentHealth = 0;

        BuildHealthbar();

        if (currentHealth <= 0)
        {
            player.GetComponent<Animator>().SetTrigger("Die");
            isAlive = false;
        }
    }

    // Build healthbar UI
    void BuildHealthbar()
    {
        if (healthbarUI == null || hpPrefab == null)
            return;

        foreach (GameObject icon in healthIcons)
        {
            if (icon != null)
                Destroy(icon);
        }

        healthIcons.Clear();

        for (int i = 0; i < currentHealth; i++)
        {
            GameObject newHP = Instantiate(hpPrefab, healthbarUI);
            healthIcons.Add(newHP);
        }
    }

    // Regenerate health
    public void RegenerateHealth(int healthRestore)
    {
        if (!isAlive) return;

        currentHealth += healthRestore;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        BuildHealthbar();
    }

    // Upgrade max health
    public void UpgradeMaxHealth(int healthUpgrade)
    {
        if (!isAlive) return;

        if (maxHealth + healthUpgrade <= maxHealthWithUpgrades)
            maxHealth += healthUpgrade;
        else
            maxHealth = maxHealthWithUpgrades;

        currentHealth = maxHealth;
        BuildHealthbar();
    }
}
