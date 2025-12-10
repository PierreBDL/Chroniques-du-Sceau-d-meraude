using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    // Object
    public Sprite ImageItem;
    public Collider2D Collider2DItem;
    public string NameItem;
    public int PriceItem;

    // Global price text panel
    public TextMeshProUGUI priceText;
    public GameObject priceTextPanel;

    // Global name text panel
    public TextMeshProUGUI nameText;
    public GameObject nameTextPanel;


    // At start, hide price text
    void Start()
    {
        priceTextPanel.SetActive(false);
        nameTextPanel.SetActive(false);

        // Load sprite item
        GetComponent<SpriteRenderer>().sprite = ImageItem;
        GetComponent<Transform>().localScale = new Vector2(12, 12);

        // Set collider size
        GetComponent<BoxCollider2D>().offset = Vector2.zero;
        GetComponent<BoxCollider2D>().size = new Vector2(0.1f, 0.2f);
    }

    // Show price when player is in trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Set the text for name and price
            nameText.text = NameItem;
            priceText.text = PriceItem.ToString();

            // Activate the panels
            priceTextPanel.SetActive(true);
            nameTextPanel.SetActive(true);
        }
    }

    // Hide price when player exits trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            priceTextPanel.SetActive(false);
            nameTextPanel.SetActive(false);
        }
    }
}
