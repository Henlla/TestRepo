using UnityEngine;

public class ObjectPlaceable : MonoBehaviour
{
    public BaseObjectSO objectLib;

    public bool Placed { get; set; }
    public BoundsInt area;
    public Vector3 origin;

    #region Build Method

    public bool CanBePlaced()
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;
        return GridBuildingSystem.Instance.CanTakeArea(areaTemp);
    }

    public void Place()
    {
        Vector3Int positionInt = GridBuildingSystem.Instance.gridLayout.LocalToCell(transform.position);
        BoundsInt areaTemp = area;
        areaTemp.position = positionInt;

        Placed = true;

        GridBuildingSystem.Instance.TakeArea(areaTemp);
    }

    public void CheckPlacement()
    {
        if (!Placed)
        {
            if (CanBePlaced())
            {
                Place();
                origin = transform.position;
            }
            //else
            //{
            //    Destroy(transform.gameObject);
            //}
        }
        else
        {
            if (CanBePlaced())
            {
                Place();
                origin = transform.position;
            }
            else
            {
                transform.position = origin;
                Place();
                GridBuildingSystem.Instance.ClearArea(area, GridBuildingSystem.Instance.TempFarm);
            }
        }
    }

    #endregion
}
