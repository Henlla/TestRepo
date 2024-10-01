using UnityEngine;

[CreateAssetMenu(fileName = "_", menuName = "SO/Shop/Item/Enclosure")]
public class EnclosureItemSO : BaseShopItemSO
{
    public EnclosureObjectSO enclosure;

    public void InitializePurchaseObject()
    {
        ShopManager.Instance.CloseShop();
        enclosure.Prefab.Placed = false;
        var tempObject = Instantiate(enclosure.Prefab.transform);
        tempObject.gameObject.AddComponent<ObjectDrag>();
        tempObject.GetComponent<ObjectPlaceable>().Placed = true;
        tempObject.GetComponent<ObjectPlaceable>().transform.position = Vector3.zero;
        var button = tempObject.gameObject.transform.Find("Button");
        if (button)
        {
            button.gameObject.SetActive(true);
        }
        Camera.main.transform.position = new Vector3(tempObject.transform.position.x, tempObject.transform.position.y, -10);
        MapManager.Instance.TurnOnEditMode();
        ShopManager.Instance.isItemFromShop = true;
    }

}