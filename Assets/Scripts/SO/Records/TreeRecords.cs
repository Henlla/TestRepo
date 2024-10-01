using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeRecords", menuName = "ScriptableObjects/TreeRecords")]
public class TreeRecords : ScriptableObject
{
    public List<TreeRecordEntry> treeRecords = new List<TreeRecordEntry>();

    [System.Serializable]
    public class TreeRecordEntry
    {
        public string treeId;  // ID cây
        public int treeState;  // Trạng thái: đã bị chặt hay chưa  0: chưa| 1: đang| 2: đã
        public int remainingChopTime;  // Thời gian chặt còn lại
    }

    // Tìm bản ghi của cây dựa trên treeId
    public TreeRecordEntry GetRecordById(string id)
    {
        return treeRecords.Find(record => record.treeId == id);
    }

    // Thêm bản ghi mới vào TreeRecords
    public void AddRecord(TreeRecordEntry newRecord)
    {
        treeRecords.Add(newRecord);
    }
}
