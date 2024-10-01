using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public enum ItemType
    {
        Tools,
        CropsAndSeeds,
        Eggs,
        Structures,
        Roads
    }

    public ItemType itemType;   // Type of the item (enum)
    public string itemName;     // Name of the item
    public int amount;          // Quantity of the item
    public Sprite itemSprite;   // 2D model of the item
    public int price;           // Price of the item (for shop purposes)

    // Constructor
    public ItemData(ItemType type, string name, int amount, Sprite sprite, int price)
    {
        this.itemType = type;
        this.itemName = name;
        this.amount = amount;
        this.itemSprite = sprite;
        this.price = price;   
    }
}
