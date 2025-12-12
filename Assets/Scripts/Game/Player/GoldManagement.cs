using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldManagement : MonoBehaviour
{
    // Current gold amount
    public static int currentGold = 0;
    
    // Print current gold amount in UI
    public GameObject MoneyQuantity;

    // Bool to check if gold value is initialized
    private static bool isInitialized = false;

    // First initialization of gold value
    void Awake ()
    {
        // Initialize gold only once
        if (!isInitialized)
        {
            currentGold = 0;
            isInitialized = true;
        } else {
            // Else update UI at start
            UpdateGoldUI();
        }
    }

    // Method to add gold
    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
    }

    // Method to deduct gold
    public void DeductGold(int amount)
    {
        if (currentGold - amount >= 0)
        {
            currentGold -= amount;
            UpdateGoldUI();
        }
    }

    // Print gold amount in UI
    public void UpdateGoldUI()
    {
        MoneyQuantity.GetComponent<TextMeshProUGUI>().text = currentGold.ToString("D3");
    }
}
