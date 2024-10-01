using UnityEditor;
using UnityEngine;

public class TreeRecordGenerator : MonoBehaviour
{
    public TreeRecords treeRecords;  // Tham chiếu tới TreeRecords (ScriptableObject)
    public string saveFolder = "Assets/Scripts/SO/Records";  // Folder để lưu các TreeRecords

    [ContextMenu("Generate Tree Records")]
    public void GenerateTreeRecordsForAllTrees()
    {
        if (treeRecords == null)
        {
            Debug.LogError("TreeRecords chưa được chỉ định!");
            return;
        }

        // Kiểm tra xem thư mục lưu trữ đã tồn tại hay chưa, nếu không thì tạo thư mục
        if (!AssetDatabase.IsValidFolder(saveFolder))
        {
            Debug.Log($"Thư mục {saveFolder} không tồn tại. Đang tạo thư mục...");
            AssetDatabase.CreateFolder("Assets/Scripts/SO", "Records");
        }

        // Tìm tất cả các đối tượng Tree trong scene
        Tree[] trees = FindObjectsOfType<Tree>();

        foreach (Tree tree in trees)
        {
            // Tạo ID duy nhất cho TreeRecordEntry
            string treeId = "Tree_" + tree.gameObject.name + "_" + tree.GetInstanceID();

            // Tạo một TreeRecordEntry mới
            TreeRecords.TreeRecordEntry newEntry = new TreeRecords.TreeRecordEntry
            {
                treeId = treeId,
                treeState = 0,  // Hoặc true nếu cây đã bị chặt
                remainingChopTime = 0  // Thời gian chặt còn lại, có thể thay đổi
            };

            // Thêm TreeRecordEntry vào TreeRecords
            treeRecords.AddRecord(newEntry);

            Debug.Log($"Generated TreeRecord for {tree.gameObject.name} with ID {treeId}");
        }

        // Lưu tất cả thay đổi trong Unity
        AssetDatabase.SaveAssets();

        // Kiểm tra lại thư mục và tạo asset
        string assetPath = $"{saveFolder}/TreeRecords.asset";
        if (!AssetDatabase.IsValidFolder(saveFolder))
        {
            Debug.LogError($"Thư mục {saveFolder} không tồn tại và không thể tạo asset!");
            return;
        }
        Debug.Log($"Saved TreeRecords asset at {assetPath}");
    }
}
