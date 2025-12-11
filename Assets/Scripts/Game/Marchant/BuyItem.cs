using UnityEngine;
using TMPro;

public class BuyItem : MonoBehaviour
{
    public GameObject PriceItem; // Text with price

    public GameObject PlayerStats; // Player stats store in GameManager

    public Collider2D Collider2DBuyItem;

    public GameObject BuyOrNotText; // Text to show if the player bought the item or not

    private bool isPlayerInRange = false;

    public Sprite ImageNoStock;


    void Start()
    {
        BuyOrNotText.SetActive(false);
    }

    void Update()
    {
        // Check for E key press when player is in range
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TryBuyItem();
        }
    }

    // When player enters trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    // When player exits trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            BuyOrNotText.GetComponentInChildren<TextMeshProUGUI>().text = "";
            BuyOrNotText.SetActive(false);
        }
    }

    void TryBuyItem()
    {
        int playerMoney = GoldManagement.currentGold;
        int itemPrice = int.Parse(PriceItem.GetComponent<TextMeshProUGUI>().text);

        // Activate the text panel
        BuyOrNotText.SetActive(true);

        // Buy Item
        if (playerMoney >= itemPrice)
        {
            // Deduct the price from player's money
            GoldManagement.currentGold -= itemPrice;
            BuyOrNotText.GetComponentInChildren<TextMeshProUGUI>().text = "Item Purchased!";

            // Remove the item from the scene
            Collider2DBuyItem.enabled = false;
            this.GetComponent<SpriteRenderer>().sprite = ImageNoStock;
            GetComponent<Transform>().localScale = new Vector2(0.05f, 0.05f);

            // Set collider size
            GetComponent<BoxCollider2D>().offset = Vector2.zero;
            GetComponent<BoxCollider2D>().size = new Vector2(12, 12);

            // Active the effect of buying item
            if (this.GetComponent<ItemManager>().NameItem == "Coeur")
            {
                // Upgrade max health by 1
                PlayerStats.GetComponentInChildren<Player_Health>().UpgradeMaxHealth(1);
            }
        } else {
            BuyOrNotText.GetComponentInChildren<TextMeshProUGUI>().text = "Not enough gold!";
        }
    }
}
