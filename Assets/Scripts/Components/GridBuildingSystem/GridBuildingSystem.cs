using UnityEngine;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : Singleton<GridBuildingSystem>
{
    public GridLayout gridLayout;
    public GameObject showGrid;
    public Tilemap MainFarm;
    public Tilemap TempFarm;
    public Tilemap DecorFarm;
    public TileBase takenTile;
    public TileBase notTakenTile;

    private Vector3 prevPos;
    private BoundsInt preArea;

    #region Tilemap management

    private TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y];
        int counter = 0;
        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }
        return array;
    }

    private void SetTilesBlock(BoundsInt area, TileBase tileBase, Tilemap tilemap)
    {
        TileBase[] tileArray = new TileBase[area.size.x * area.size.y];
        FillTiles(tileArray, tileBase);
        tilemap.SetTilesBlock(area, tileArray);
    }

    private void FillTiles(TileBase[] arr, TileBase tileBase)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = tileBase;
        }
    }

    public void ShowGrid(bool value)
    {
        showGrid.SetActive(value);
    }

    #endregion

    #region Building Placement

    public void FollowBuilding(ObjectPlaceable building)
    {
        ClearArea(preArea, TempFarm);
        building.area.position = gridLayout.WorldToCell(building.gameObject.transform.position);
        BoundsInt buildingArea = building.area;
        TileBase[] baseArray = GetTilesBlock(buildingArea, MainFarm);
        TileBase[] tileArray = new TileBase[baseArray.Length];
        for (int i = 0; i < baseArray.Length; i++)
        {
            if (baseArray[i] == null)
            {
                tileArray[i] = notTakenTile;
            }
            else
            {
                FillTiles(tileArray, takenTile);
                break;
            }
        }
        TempFarm.SetTilesBlock(buildingArea, tileArray);
        preArea = buildingArea;
    }

    public void ClearArea(BoundsInt area, Tilemap tilemap)
    {
        SetTilesBlock(area, null, tilemap);
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] baseArray = GetTilesBlock(area, MainFarm);
        foreach (var b in baseArray)
        {
            if (b == takenTile)
            {
                return false;
            }
        }
        return true;
    }

    public void TakeArea(BoundsInt area)
    {
        SetTilesBlock(area, takenTile, MainFarm);
    }

    public void MoveSmooth(ObjectPlaceable temp)
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int cellPos = gridLayout.LocalToCell(touchPos);
        if (prevPos != cellPos)
        {
            temp.transform.localPosition = gridLayout.CellToLocalInterpolated(cellPos
                + new Vector3(-0.5f, -0.5f, 0f));
            prevPos = cellPos;
        }
    }

    public void ChangeTempTileMapAlpha(float value)
    {
        Color currentColor = TempFarm.color;
        currentColor.a = value;
        TempFarm.color = currentColor;
    }

    public void ChangeMainTileMapAlpha(float value)
    {
        Color currentColor = MainFarm.color;
        currentColor.a = value;
        MainFarm.color = currentColor;
    }

    #endregion
}
