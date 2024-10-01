using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "DecorateRecords", menuName = "SO/Decorate/Records")]
public class DecorateRecords : BaseRecordSO<DecorateData>
{
    public override List<DecorateData> GetList()
    {
        return base.GetList();
    }

    public void SaveList(List<DecorateData> listData)
    {
        ListObject.AddRange(listData);
    }

    public void Remove(Vector3Int position, TileBase tileBase)
    {
        var existsTile = ListObject.Find(x => x.position == position && x.tileBase == tileBase);
        if (existsTile == null)
        {
            ListObject.Remove(existsTile);
        }
    }

}
