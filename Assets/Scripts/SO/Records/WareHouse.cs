using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "WareHouseRecord", menuName = "SO/WareHouseRecord")]
public class WareHouse : ScriptableObject
{
    public List<ItemData> ItemList;

    #region method of warehouse
    public bool AddWareHouseItem(string ItemName, ItemType type, float quantity, Sprite sprite)
    {
        ItemData Item = Search(ItemName);
        try
        {
            if (Item != null && Item.type == type)
            {
                Item.quantity += quantity;

            }
            else
            {
                Item = new ItemData
                {
                    itemName = ItemName,
                    type = type,
                    quantity = quantity,
                    sprite = sprite
                };
                ItemList.Add(Item);
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.Log("Error adding item to inventory: " + ex.Message); // Thêm log để theo dõi lỗi
            return false;
        }
    }

    public ItemData Search(string itemName)
    {
        foreach (ItemData item in ItemList)
        {
            if (item.itemName.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                return item;
            }
        }
        Debug.Log($"Item '{itemName}' not found in the warehouse.");
        return null;
    }

    #endregion
}

// public enum ItemType
// {
//     Corn,
//     Rice,
//     Mango,
//     Coconut,
//     Appale,
//     Fertilizer
// }
