using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ItemData 
{
    public string itemName;
    public ItemType type;
    public float quantity;
    public Sprite sprite;
<<<<<<< HEAD
    public float price;
    public ItemData(ItemType type, string itemName, float quantity, Sprite sprite, float price )
    {
        this.type = type;
        this.itemName = itemName;
        this.quantity = quantity;
        this.sprite = sprite;
        this.price = price;
    }
=======
>>>>>>> parent of d3ed20d (Merge pull request #7 from thaiyud/dev)

}
