using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private ShopCard shopCardPrefabs;
    [SerializeField] private ShopObject shopObject;
    public WareHouse wareHouse;
    [SerializeField] private Player player;

    public bool isItemFromShop;

    private bool isShopOpen;

    public void OpenShop()
    {
        if (isShopOpen)
        {
            return;
        }
        UIManager.Instance.ShopPanel.SetActive(true);
        // LoadShop();
        LoadShop(new List<ItemType> { ItemType.Seeds, ItemType.Fertilizer });
        CameraMovement.Instance.SetCameraMoveDisable(true);
        isShopOpen = true;
    }

    public void CloseShop()
    {
        if (!isShopOpen)
        {
            return;
        }
        UIManager.Instance.ShopPanel.SetActive(false);
        CameraMovement.Instance.SetCameraMoveDisable(false);
        RemoveItemWhenClosePanel();
        isShopOpen = false;
    }

    // public void LoadShop()
    // {
    //     foreach (var item in shopObject.listItem)
    //     {
    //         ShopCard card = Instantiate(shopCardPrefabs, UIManager.Instance.ShopContainer);
    //         card.ConfigShopCard(item);
    //     }
    // }

    public void LoadShop(List<ItemType> types)
    {
        RemoveItemWhenClosePanel();
        foreach (var item in shopObject.listItem)
        {
            if (types.Contains(item.ItemType))
            {
                ShopCard card = Instantiate(shopCardPrefabs, UIManager.Instance.ShopContainer);
                card.ConfigShopCard(item);
            }
        }
    }

    public void RemoveItemWhenClosePanel()
    {
        foreach (Transform item in UIManager.Instance.ShopContainer.transform)
        {
            Destroy(item.gameObject);
        }
    }

    public void PurchaseItem(BaseShopItemSO item)
    {
        switch (item.ItemType)
        {
            case ItemType.Decorate:
                DecorateItemSO decorateItemSO = item as DecorateItemSO;
                DecorateManager.Instance.InitializePurchaseObject(decorateItemSO);
                break;
            case ItemType.Enclosure:
                EnclosureItemSO enclosureItem = item as EnclosureItemSO;
                enclosureItem.InitializePurchaseObject();
                break;
            case ItemType.Factory:
                break;
            case ItemType.Seeds:
                wareHouse.AddWareHouseItem(item.name, item.ItemType, 1, item.ItemIcon);
                break;
            case ItemType.Fertilizer:
                wareHouse.AddWareHouseItem(item.name, item.ItemType, 1, item.ItemIcon);
                break;
            default:
                break;
        }
    }

    public void LoadSeednFertilizereItems()
    {
        LoadShop(new List<ItemType> { ItemType.Seeds, ItemType.Fertilizer });
    }
    public void LoadEnclosureItems()
    {
        LoadShop(new List<ItemType> { ItemType.Enclosure });
    }
    public void LoadRoadItems()
    {
        LoadShop(new List<ItemType> { ItemType.Road, ItemType.Decorate });
    }
    public void LoadToolItems()
    {
        LoadShop(new List<ItemType> { ItemType.Tool });
    }
}
