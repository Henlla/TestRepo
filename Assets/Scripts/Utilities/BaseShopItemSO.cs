using System;
using UnityEngine;

public class BaseShopItemSO : ScriptableObject
{
    private string ItemId;
    public string ItemName;
    public float ItemPrice;
    public Sprite ItemIcon;
    public ItemType ItemType;

    private void OnEnable()
    {
        if (string.IsNullOrEmpty(ItemId))
        {
            ItemId = Guid.NewGuid().ToString("N");
        }
    }
}

public enum ItemType
{
    Decorate,
    Enclosure,
    Factory,

    Fertilizer,
    Seeds,
    Road,
    Tool
}
