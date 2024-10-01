using UnityEngine;

public class WoodItem : MonoBehaviour
{
    public ResourceRecords resourceRecords;  // Tham chiếu tới ResourceRecords
    public string woodItemName = "Wood";  // Tên của vật phẩm gỗ
    public int woodAmount = 1;  // Số lượng gỗ thu hoạch khi nhặt được

    private void OnMouseDown()
    {
        // Kiểm tra nếu con trỏ chuột nhấn vào vật phẩm gỗ
        AddToResourceRecords();  // Thêm vật phẩm gỗ vào ResourceRecords
        Destroy(gameObject);  // Hủy vật phẩm gỗ sau khi nhặt
    }

    // Thêm vật phẩm gỗ vào ResourceRecords
    private void AddToResourceRecords()
    {
        // Kiểm tra nếu đã tồn tại bản ghi của vật phẩm gỗ
        var existingRecord = resourceRecords.GetRecordByname(woodItemName);

        if (existingRecord != null)
        {
            // Nếu vật phẩm gỗ đã có trong danh sách, tăng số lượng
            existingRecord.quantity += woodAmount;
        }
        else
        {
            // Nếu vật phẩm chưa có, thêm mới
            ResourceRecords.ResourceRecordEntry newRecord = new ResourceRecords.ResourceRecordEntry
            {
                typeItem = "Resource",
                nameItem = woodItemName,
                quantity = woodAmount
            };

            resourceRecords.AddRecord(newRecord);  // Thêm vào ResourceRecords
        }
    }
}
