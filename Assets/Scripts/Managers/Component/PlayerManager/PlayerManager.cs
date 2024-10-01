using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Danh sách các mục trong kho
    public List<ItemData> inventoryItems;

    // Thêm mục vào kho
    public bool AddInventoryItem(string treeType, string treeName, float quantity, Sprite icon)
    {
        //ItemData newItem = new ItemData
        //{
        //    treeType = treeType,
        //    treeName = treeName,
        //    quantity = quantity,
        //    icon = icon
        //};

        //if (inventoryItems == null)
        //{
        //    inventoryItems = new List<ItemData>();
        //}
        //try
        //{
        //    inventoryItems.Add(newItem);
        //    return true;
        //}
        //catch (Exception ex)
        //{
        //    Debug.LogError("Error adding item to inventory: " + ex.Message); // Thêm log để theo dõi lỗi
        //   return false;
        //}
        return true;
    }
}

