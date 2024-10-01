using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemCost;

    private BaseShopItemSO item;

    public void ConfigShopCard(BaseShopItemSO shopItem)
    {
        item = shopItem;
        itemIcon.sprite = shopItem.ItemIcon;
        itemName.text = shopItem.ItemName;
        itemCost.text = shopItem.ItemPrice.ToString();
    }

    public void PurchaseItem()
    {
        switch (item.ItemType)
        {
            case ItemType.Decorate:
                DecorateManager.Instance.InitializePurchaseObject(item as DecorateItemSO);
                break;
            case ItemType.Enclosure:
                EnclosureItemSO enclosureItem = item as EnclosureItemSO;
                enclosureItem.InitializePurchaseObject();
                break;
            case ItemType.Factory:
                break;
            default:
                break;
        }
    }
}