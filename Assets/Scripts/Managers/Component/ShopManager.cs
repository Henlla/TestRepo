using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    [SerializeField] private ShopCard shopCardPrefabs;
    [SerializeField] private Transform shopContainer;
    [SerializeField] private ShopObject shopObject;
    public WareHouse wareHouse;
    [SerializeField] private Player player;

    public bool isItemFromShop;

    private bool isShopOpen;

    public void OpenShop()
    {
        LoadShop();
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
        for (int i = 0; i < items.Length; i++)
        {
            ShopCard card = Instantiate(shopCardPrefabs,shopContainer);
            card.ConfigShopCard(items[i]);
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

}
