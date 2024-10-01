using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DecorateManager : Singleton<DecorateManager>
{
    [Header("Config")]
    [SerializeField] private DecorateRecords decorateRecords;
    private DecorateItemSO decorateItem;

    private List<DecorateData> tempList;
    private Vector3Int startPos;
    private Vector3Int endPos;
    private Vector3Int lastPos;

    private int totalBlock;

    #region Unity Method

    private void Start()
    {

        tempList = tempList ?? new List<DecorateData>();
        tempList.Clear();
        LoadDecorate();
    }

    private void Update()
    {
        if (!MapManager.Instance.IsDecorEditMode())
        {
            return;
        }

        if (!decorateItem)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0) && startPos == Vector3.zero)
        {
            ClickToDecor();
        }
        if (Input.GetMouseButton(0))
        {
            DragToDecor();
        }
        if (Input.GetMouseButtonUp(0))
        {
            EndPosition();
        }
    }

    #endregion

    #region Purchase From Shop
    public void InitializePurchaseObject(DecorateItemSO item)
    {
        decorateItem = item;
        ShopManager.Instance.CloseShop();
        MapManager.Instance.TurnOnDecorMode();
    }

    #endregion

    #region Decorate map

    public void ClickToDecor()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = GridBuildingSystem.Instance.DecorFarm.WorldToCell(mousePosition);
        if (!IsEmptyPlace())
        {
            return;
        }
        startPos = cellPos;
        lastPos = cellPos;
        GridBuildingSystem.Instance.DecorFarm.SetTile(cellPos, decorateItem.tileBase);
        totalBlock++;
        AddDataToList(cellPos, decorateItem.tileBase);
    }

    public void DragToDecor()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = GridBuildingSystem.Instance.DecorFarm.WorldToCell(mousePosition);
        if (IsEmptyPlace())
        {
            lastPos = cellPos;
            GridBuildingSystem.Instance.DecorFarm.SetTile(cellPos, decorateItem.tileBase);
            totalBlock++;
            AddDataToList(cellPos, decorateItem.tileBase);
        }
    }

    public bool IsEmptyPlace()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = GridBuildingSystem.Instance.DecorFarm.WorldToCell(mousePosition);
        return GridBuildingSystem.Instance.DecorFarm.GetTile(cellPos) == null;
    }

    public void EndPosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = GridBuildingSystem.Instance.DecorFarm.WorldToCell(mousePosition);
        endPos = cellPos;
        lastPos = cellPos;

        Debug.Log($"start: {startPos}");
        Debug.Log($"end: {endPos}");
        Debug.Log($"totalBlock: {totalBlock}");
        Debug.Log($"List block: {tempList.Count}");
    }

    public void AddDataToList(Vector3Int position, TileBase tileBase)
    {
        tempList.Add(new DecorateData
        {
            position = position,
            tileBase = tileBase
        });
    }

    #endregion

    #region Load / Save / Destroy

    public void SaveDecorate()
    {
        decorateRecords.SaveList(tempList);
        MapManager.Instance.TurnOffDecorMode();
    }

    public void LoadDecorate()
    {
        var listDecor = decorateRecords.GetList();
        foreach (var item in listDecor)
        {
            GridBuildingSystem.Instance.DecorFarm.SetTile(item.position, item.tileBase);
        }
    }

    public void DestroyTileBase(Vector3Int posistion, TileBase tileBase)
    {
        GridBuildingSystem.Instance.DecorFarm.SetTile(posistion, null);
        decorateRecords.Remove(posistion, tileBase);
    }

    #endregion

    #region Confirm / Denied Decor

    public void Confirm()
    {
        GridBuildingSystem.Instance.DecorFarm.SetTile(lastPos, null);
        tempList.Remove(tempList.Find(x => x.position == lastPos && x.tileBase == decorateItem.tileBase));
        float totalPrice = decorateItem.ItemPrice * totalBlock;
        // tru tien cho player
        SaveDecorate();
        tempList.Clear();
    }

    public void Denied()
    {
        foreach (var item in tempList)
        {
            GridBuildingSystem.Instance.DecorFarm.SetTile(item.position, null);
        }
        decorateItem = null;
        tempList.Clear();
        MapManager.Instance.TurnOffDecorMode();
    }

    #endregion
}
