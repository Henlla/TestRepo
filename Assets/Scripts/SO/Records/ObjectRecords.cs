using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ObjectRecords", menuName = "SO/Object/Records")]
public class ObjectRecords : BaseRecordSO<ObjectData>
{
    public override List<ObjectData> GetList()
    {
        return base.GetList();
    }

    public override void SaveList(ObjectData obj)
    {
        base.SaveList(obj);
    }
}