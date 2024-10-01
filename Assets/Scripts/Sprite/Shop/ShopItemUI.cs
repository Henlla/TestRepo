using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    public TMP_Text itemName;                   // Reference to the item's name UI element
    public Image itemIcon;                       // Reference to the item's icon UI element
    public TMP_Text itemPrice;                   // Reference to the item's price UI element
    public Button purchaseButton;                // Reference to the purchase button
    private Player playerData;                   // Reference to the Player ScriptableObject

    // Method to set the item details in the shop UI
    public void SetItemDetails(ItemData itemData, Player player)
    {
        itemName.text = itemData.itemName;                  // Set the item name
        itemIcon.sprite = itemData.itemSprite;              // Set the item sprite
        itemPrice.text = itemData.price.ToString();         // Set the item price
        playerData = player;                                 // Reference to player data

        // Set up the purchase action on the button
        purchaseButton.onClick.RemoveAllListeners();         // Clear previous listeners to avoid multiple calls
        purchaseButton.onClick.AddListener(() => PurchaseItem(itemData)); // Add purchase listener
    }

    // Method to handle item purchase
    private void PurchaseItem(ItemData itemData)
    {
        if (playerData.money >= itemData.price)
        {
            playerData.money -= itemData.price;

            // Add item to the warehouse using the existing WarehouseController
            WarehouseController warehouseController = FindObjectOfType<WarehouseController>();
            if (warehouseController != null)
            {
                // Create a new ItemData object for adding to the warehouse
                ItemData newItemData = new ItemData(itemData.itemType, itemData.itemName, 1, itemData.itemSprite, itemData.price);
                warehouseController.AddItem(newItemData);  // Add the item to the warehouse
            }
        }
        else
        {
            Debug.Log("Not enough money to purchase the item!");
        }
    }
}