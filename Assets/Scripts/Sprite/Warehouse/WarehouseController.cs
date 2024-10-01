using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseController : MonoBehaviour
{
    public Warehouse warehouseData; // Reference to the Warehouse ScriptableObject
    public Player playerData;       // Reference to Player ScriptableObject (for inventory limit or other interactions)

    private void Start()
    {
        // Load Warehouse and Player if not set via inspector
        if (warehouseData == null)
        {
            warehouseData = Resources.Load<Warehouse>("Warehouse");
        }

        if (playerData == null)
        {
            playerData = Resources.Load<Player>("Player");
        }
    }

    // Add item to the warehouse
    public void AddItem(ItemData newItemData)
    {
        // Check if the player has room in the warehouse (based on player level, warehouse limits, etc.)
        if (warehouseData.items.Count >= playerData.inventoryLimit)
        {
            Debug.Log("Not enough space in the warehouse.");
            return;
        }

        // Check if the item already exists in the warehouse
        ItemData existingItem = warehouseData.items.Find(i => i.itemName == newItemData.itemName);

        if (existingItem != null)
        {
            // Item exists, just increase the amount
            existingItem.amount += newItemData.amount;
        }
        else
        {
            // Add the new item to the warehouse
            warehouseData.items.Add(newItemData);
        }

        // Optionally, save changes to the warehouse
        SaveWarehouse();
    }


    // Remove item from the warehouse
    public bool RemoveItem(string itemName, int itemAmount, int itemLevel)
    {
        // Find the item in the warehouse
        ItemData existingItem = warehouseData.items.Find(i => i.itemName == itemName);

        if (existingItem != null)
        {
            // Check if there are enough items to remove
            if (existingItem.amount >= itemAmount)
            {
                existingItem.amount -= itemAmount;

                // Remove the item if the amount reaches zero
                if (existingItem.amount == 0)
                {
                    warehouseData.items.Remove(existingItem);
                }

                // Optionally, save changes to the warehouse
                SaveWarehouse();
                return true;
            }
            else
            {
                Debug.Log($"Not enough {itemName} in the warehouse.");
                return false;
            }
        }
        else
        {
            Debug.Log($"{itemName} (Level {itemLevel}) not found in the warehouse.");
            return false;
        }
    }

    // Save the warehouse data (for persistence)
    private void SaveWarehouse()
    {
#if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(warehouseData);
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    public List<ItemData> GetSortedItems()
    {
        // Sort the items in descending order based on their amount
        List<ItemData> sortedItems = new List<ItemData>(warehouseData.items);
        sortedItems.Sort((item1, item2) => item2.amount.CompareTo(item1.amount));

        return sortedItems;
    }

}
