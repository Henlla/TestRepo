using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarehouseUI : MonoBehaviour
{
    public Warehouse warehouseData;         // Reference to the warehouse data (ScriptableObject)
    public GameObject itemTemplatePrefab;   // The prefab for displaying individual warehouse items (set in Inspector)
    public Transform contentParent;         // Parent object for item templates (set in Inspector, should be the Content of ScrollView)

    public Button closeButton;              // Reference to the Close button
    public Button openButton;               // Button to open the warehouse
    public GameObject warehousePanel;       // The actual panel displaying the warehouse UI

    private void Start()
    {
        // Attach the close button functionality
        closeButton.onClick.AddListener(CloseWarehouse);

        // Attach the open button functionality
        openButton.onClick.AddListener(OpenWarehouse);

        // Initially, the warehouse is closed
        warehousePanel.SetActive(false);

        // Load warehouse data if not set in the inspector
        if (warehouseData == null)
        {
            warehouseData = Resources.Load<Warehouse>("Warehouse");
        }

    }

    public void OpenWarehouse()
    {
        if (!warehousePanel.activeSelf)  // Only open if it's not already open
        {
            warehousePanel.SetActive(true);
            Debug.Log("Opening Warehouse");
            LoadWarehouseItems();  // Load items when warehouse is opened
        }
    }

    public void CloseWarehouse()
    {
        if (warehousePanel.activeSelf)  // Only close if it's currently open
        {
            warehousePanel.SetActive(false);
            Debug.Log("Closing Warehouse");
        }
    }

    public void LoadWarehouseItems()
    {
        // Clear out the existing items from the contentParent in case of a refresh
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        // Get sorted items (sorted by amount in descending order)
        List<ItemData> sortedItems = warehouseData.items;
        sortedItems.Sort((item1, item2) => item2.amount.CompareTo(item1.amount));

        // Loop through each item in the warehouse and create UI elements for it
        foreach (ItemData item in sortedItems)
        {
            // Instantiate a new item template prefab as a child of the content parent
            GameObject newItem = Instantiate(itemTemplatePrefab, contentParent);

            // Find and set the UI components inside the item template
            Image itemImage = newItem.transform.Find("Image").GetComponent<Image>();
            TMP_Text itemAmountText = newItem.transform.Find("Text (TMP)").GetComponent<TMP_Text>();

            // Set the item's image and amount
            itemImage.sprite = item.itemSprite;
            itemAmountText.text = item.amount.ToString();
        }
    }
}
