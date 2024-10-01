using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceRecords", menuName = "ScriptableObjects/ResourceRecords")]
public class ResourceRecords : ScriptableObject
{
    public List<ResourceRecordEntry> resourceRecords = new List<ResourceRecordEntry>();

    [System.Serializable]
    public class ResourceRecordEntry
    {
        public string typeItem;  // Type of Item
        public string nameItem;  //Name of Item
        public int quantity;  
    }

    // Tìm bản ghi của vật phẩm dựa trên tên vật phẩm
    public ResourceRecordEntry GetRecordByname(string nameItem)
    {
        return resourceRecords.Find(record => record.nameItem == nameItem);
    }

    // Thêm bản ghi mới vào ResourceRecords
    public void AddRecord(ResourceRecordEntry newRecord)
    {
        resourceRecords.Add(newRecord);
    }
}
