using System.Linq;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    [SerializeField] private ObjectRecords records;

    #region Unity Method

    private void Start()
    {
        LoadMap();
    }

    #endregion

    #region Load/Save Object

    public void SaveMap(ObjectPlaceable building)
    {
        Vector3 position = building.transform.position;

        ObjectData data = new ObjectData
        {
            ObjectItem = building.objectLib,
            Prefab = building.objectLib.Prefab
        };

        var itemExists = records.ListObject.FirstOrDefault(i => i.ObjectItem.GetId().Equals(data.ObjectItem.GetId()));
        if (itemExists != null)
        {
            itemExists.Prefab.transform.position = position;
        }
        else
        {
            records.ListObject.Add(data);
        }
    }

    public void LoadMap()
    {
        foreach (var item in records.ListObject)
        {
            var objectTemp = Instantiate(item.Prefab.transform, transform);
            objectTemp.GetComponent<ObjectPlaceable>().CheckPlacement();
            objectTemp.GetComponent<ObjectPlaceable>().origin = objectTemp.transform.position;
            objectTemp.gameObject.name = objectTemp.gameObject.name.Replace("(Clone)", " ").Trim();
        }
    }

    #endregion
}
