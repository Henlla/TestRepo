using UnityEngine;

public class ButtonInteractable : Interactable
{
    private Vector3 origin;

    public override void OnClicked()
    {
        if (gameObject.name == "Confirm")
        {
            Confirm();
        }
        else if (gameObject.name == "Denied")
        {
            Denied();
        }
    }

    public override void SetActiveAndInteractable(GameObject obj, bool state)
    {
        base.SetActiveAndInteractable(obj, state);
    }

    public void ClearArea(ObjectPlaceable data)
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.gridLayout.WorldToCell(data.transform.position);
        BoundsInt areaTemp = data.area;
        areaTemp.position = positionInt;

        GridBuildingSystem.Instance.ClearArea(areaTemp, GridBuildingSystem.Instance.TempFarm);
    }

    #region Button Action
    
    public void Confirm()
    {
        var data = transform.parent.parent.GetComponent<ObjectPlaceable>();
        data.CheckPlacement();
        ObjectManager.Instance.SaveMap(data);
        MapManager.Instance.TurnOffEditMode();
        gameObject.transform.parent.gameObject.SetActive(false);
        ShopManager.Instance.isItemFromShop = false;
        origin = transform.parent.parent.gameObject.transform.position;
        ObjectManager.Instance.SaveMap(data.GetComponent<ObjectPlaceable>());
    }

    public void Denied()
    {
        var data = transform.parent.parent;
        if (ShopManager.Instance.isItemFromShop)
        {
            ClearArea(data.GetComponent<ObjectPlaceable>());
            ShopManager.Instance.isItemFromShop = false;
            Destroy(data.gameObject);
            MapManager.Instance.TurnOffEditMode();
            return;
        }
        data.GetComponent<ObjectPlaceable>().transform.position = origin;
        MapManager.Instance.TurnOffEditMode();
        gameObject.transform.parent.gameObject.SetActive(false);
    }
    
    #endregion

}
