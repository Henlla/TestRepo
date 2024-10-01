using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public GameObject ShopPanel;  // Reference to the shop UI panel
    public GameObject itemPrefab; // Prefab to display each shop item
    public Transform itemContainer; // The container to hold shop items (where prefabs will be instantiated)

    public Player playerData;     // Reference to the Player ScriptableObject
    public Shop shopData; // Reference to the Shop ScriptableObject


    public Button toolsButton;
    public Button cropsAndSeedsButton;
    public Button eggsButton;
    public Button structuresButton;
    public Button roadsButton;

    public List<ItemData> shopItems; // List of all available shop items

    void Start()
    {
        // Load player data
        playerData = Resources.Load<Player>("Player");

        // Load shop data
        shopData = Resources.Load<Shop>("Shop");

        // Check if shopData is loaded
        if (shopData == null)
        {
            Debug.LogError("Shop data could not be loaded! Please ensure the asset is in the Resources folder.");
            return; // Exit the method if the shop data is not found
        }

        // Populate the shopItems list
        shopItems = shopData.shopItems;

        // Debug log to check if items are loaded
        Debug.Log("Loaded shop items: " + shopItems.Count);
        foreach (var item in shopItems)
        {
            Debug.Log($"Item: {item.itemName}, Price: {item.price}");
        }

        // Initially hide the shop UI
        ShopPanel.SetActive(false);

        // Add listeners for the category buttons
        toolsButton.onClick.AddListener(() => DisplayItems(ItemData.ItemType.Tools));
        cropsAndSeedsButton.onClick.AddListener(() => DisplayItems(ItemData.ItemType.CropsAndSeeds));
        eggsButton.onClick.AddListener(() => DisplayItems(ItemData.ItemType.Eggs));
        structuresButton.onClick.AddListener(() => DisplayItems(ItemData.ItemType.Structures));
        roadsButton.onClick.AddListener(() => DisplayItems(ItemData.ItemType.Roads));

        // Display Tools by default
        DisplayItems(ItemData.ItemType.Tools);
    }


    // Method to open the shop
    public void OpenShop()
    {
        ShopPanel.SetActive(true);
        // Optionally refresh the UI if needed
        DisplayItems(ItemData.ItemType.Tools); // Show tools by default
    }

    // Method to close the shop
    public void CloseShop()
    {
        ShopPanel.SetActive(false);
    }

    // Method to display items in the shop by item type
    public void DisplayItems(ItemData.ItemType itemType)
    {
        // Clear existing items in the container
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // Check if shopItems is null or empty
        if (shopItems == null || shopItems.Count == 0)
        {
            Debug.LogWarning("No shop items available to display.");
            return; // Exit if there are no items to display
        }

        // Loop through all shop items and display the ones matching the selected itemType
        foreach (ItemData item in shopItems)
        {
            if (item.itemType == itemType)
            {
                // Instantiate a new item prefab
                GameObject newItem = Instantiate(itemPrefab, itemContainer);

                // Set up the prefab's details (name, sprite, price, etc.)
                ShopItemUI itemUI = newItem.GetComponent<ShopItemUI>();
                itemUI.SetItemDetails(item, playerData); // Use the ItemData object directly
            }
        }
    }
}
