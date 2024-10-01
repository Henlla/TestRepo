using System.Collections;
using TreeEditor;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject chopPanel;  // Panel chứa ChopBtn và DeselectBtn
    public GameObject cancelChopPanel;  // Panel chứa CancelChopBtn và DeselectBtn

    public static Tree selectedTree;  // Cây hiện đang được chọn
    private bool isChopping = false;  // Trạng thái cây đang bị chặt

    public Timer chopTimerPrefab;  // Prefab của Timer
    private Timer activeChopTimer;  // Tham chiếu tới Timer hiện tại

    public TreeRecords treeRecordManager;  // Tham chiếu tới TreeRecords (ScriptableObject)
    public TreeRecords.TreeRecordEntry treeRecord;  // Bản ghi của cây này

    private Canvas mainCanvas;  // Tham chiếu tới Canvas chính
    private RectTransform canvasRectTransform;  // RectTransform của Canvas

    public Sprite stumpSprite;  // Hình ảnh gốc cây sau khi chặt
    public Sprite treeSprite;   // Hình ảnh cây ban đầu (dạng cây mọc lại)

    public GameObject woodItemPrefab;  // Prefab của vật phẩm gỗ (sử dụng hình ảnh sprite)
    public int woodDropAmount = 5;  // Số lượng gỗ rơi ra

    public float regrowTime = 5f;  // Thời gian để cây mọc lại (10 giây)
    private void Awake()
    {
        if (treeRecordManager == null)
        {
            Debug.LogError("TreeRecordManager is not assigned!");
            return;
        }

        string treeId = "Tree_" + gameObject.name + "_" + GetInstanceID();
        treeRecord = treeRecordManager.GetRecordById(treeId);

        if (treeRecord == null)
        {
            treeRecord = new TreeRecords.TreeRecordEntry
            {
                treeId = treeId,
                treeState = 0,
                remainingChopTime = 0
            };
            treeRecordManager.AddRecord(treeRecord);
        }
    }

    private void Start()
    {
        mainCanvas = FindObjectOfType<Canvas>();
        canvasRectTransform = mainCanvas.GetComponent<RectTransform>();

        if (treeRecord.treeState == 2)
        {
            Destroy(gameObject);  // Nếu cây đã bị chặt
        }
        else if (treeRecord.treeState == 1 && treeRecord.remainingChopTime > 0)
        {
            StartChopping(treeRecord.remainingChopTime);  // Tiếp tục chặt nếu đang chặt
        }

    }

    private void OnMouseDown()
    {
        // Hủy chọn cây cũ trước khi chọn cây mới
        if (selectedTree != null && selectedTree != this)
        {
            selectedTree.DeselectTree();  // Hủy chọn cây trước đó
        }

        // Đặt cây hiện tại làm cây được chọn
        selectedTree = this;
        isChopping = (treeRecord.treeState == 1);  // Cập nhật trạng thái đang chặt nếu cần
        ShowRelevantPanel();  // Hiển thị panel tương ứng
    }

    // Hiển thị panel phù hợp dựa trên trạng thái của cây
    private void ShowRelevantPanel()
    {
        if (treeRecord.treeState == 0)
        {
            ShowPanel(chopPanel);  // Hiển thị ChopPanel nếu chưa chặt
            cancelChopPanel.SetActive(false);  // Ẩn CancelChopPanel
        }
        else if (treeRecord.treeState == 1)
        {
            ShowPanel(cancelChopPanel);  // Hiển thị CancelChopPanel nếu đang chặt
            chopPanel.SetActive(false);  // Ẩn ChopPanel
        }
    }

    public void StartChopping(int chopTime)
    {
        treeRecord.treeState = 1;
        treeRecord.remainingChopTime = chopTime;

        // Tạo Timer cho cây hiện tại
        activeChopTimer = Instantiate(chopTimerPrefab, mainCanvas.transform);

        // Đặt Timer cho cây hiện tại và bắt đầu đếm thời gian
        activeChopTimer.SetTree(this);
        activeChopTimer.Begin(chopTime);

        // Đảm bảo Timer xuất hiện đúng vị trí cây
        UpdateTimerPosition();

        // Cập nhật giao diện Panel
        ShowRelevantPanel();
    }

    public void CancelChopping()
    {
        if (isChopping && treeRecord.treeState == 1)
        {
            treeRecord.treeState = 0;  // Đặt trạng thái cây về chưa chặt
            treeRecord.remainingChopTime = 0;  // Reset thời gian chặt

            if (activeChopTimer != null)
            {
                Destroy(activeChopTimer.gameObject);  // Hủy Timer nếu có
            }

            isChopping = false;  // Đặt lại trạng thái cây
            ShowRelevantPanel();  // Cập nhật lại giao diện
        }
    }

    public void DeselectTree()
    {
        // Reset trạng thái cây khi hủy chọn
        isChopping = false;  // Đặt lại trạng thái chặt
        selectedTree = null;  // Xóa cây được chọn

        // Ẩn tất cả các panel vì không còn cây nào được chọn
        chopPanel.SetActive(false);
        cancelChopPanel.SetActive(false);
    }

    public void OnChopComplete()
    {
        treeRecord.treeState = 2;  // Đánh dấu cây đã bị chặt

        // Chuyển hình dạng của cây thành gốc cây
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && stumpSprite != null)
        {
            spriteRenderer.sprite = stumpSprite;  // Đổi hình dạng thành gốc cây
        }

        // Xóa Timer sau khi chặt xong
        if (activeChopTimer != null)
        {
            Destroy(activeChopTimer.gameObject);  // Hủy Timer
        }

        selectedTree = null;

        // Gọi hàm để tạo vật phẩm gỗ sau khi chặt xong
        DropWoodItems();

        // Bắt đầu Coroutine để cây mọc lại sau 10 giây
        StartCoroutine(RegrowTree());
    }

    private void UpdateTimerPosition()
    {
        if (activeChopTimer != null)  // Kiểm tra nếu Timer đang hoạt động
        {
            // Lấy vị trí cây trong không gian thế giới và chuyển đổi nó sang tọa độ màn hình (Screen Space)
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1, 0));

            // Đảm bảo RectTransform của Timer đã được lấy chính xác
            RectTransform timerRect = activeChopTimer.GetComponent<RectTransform>();

            // Chuyển đổi vị trí từ Screen Space sang Local Space của Canvas
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRectTransform,  // Đảm bảo dùng đúng RectTransform của Canvas
                screenPosition,  // Vị trí màn hình (Screen Space) của cây
                mainCanvas.worldCamera,  // Camera của Canvas
                out Vector2 localPoint  // Vị trí Local Space trong Canvas
            );

            // Đặt vị trí cho Timer theo đúng vị trí của cây
            timerRect.anchoredPosition = localPoint;
        }
    }

    // Hàm để hiển thị Panel tại đúng vị trí cây
    private void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);

        // Lấy vị trí cây trong không gian thế giới và chuyển đổi nó sang Screen Space
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);

        // Chuyển đổi Screen Space sang Local Space của Canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,  // Đảm bảo dùng đúng RectTransform của Canvas
            screenPosition,  // Vị trí màn hình (Screen Space)
            mainCanvas.worldCamera,  // Camera của Canvas
            out Vector2 localPoint  // Vị trí Local Space
        );

        // Đảm bảo RectTransform của Panel được lấy chính xác
        RectTransform panelRect = panel.GetComponent<RectTransform>();

        // Đặt vị trí cho Panel tại vị trí tương ứng với cây
        panelRect.anchoredPosition = localPoint;
    }


    // Hàm để tạo ra các vật phẩm gỗ sau khi chặt xong
    private void DropWoodItems()
    {
        if (woodItemPrefab == null)
        {
            Debug.LogError("WoodItemPrefab is not assigned!");  // Log nếu Prefab chưa được gán
            return;
        }

        Debug.Log("Dropping wood items...");  // Log khi bắt đầu thả vật phẩm

        for (int i = 0; i < woodDropAmount; i++)
        {
            // Tạo vị trí ngẫu nhiên xung quanh cây để thả gỗ
            Vector3 dropPosition = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);

            Debug.Log($"Dropping wood at position {dropPosition}");  // Log vị trí thả vật phẩm

            // Tạo vật phẩm gỗ từ prefab tại vị trí thả
            Instantiate(woodItemPrefab, dropPosition, Quaternion.identity);
        }
    }

    // Coroutine để cây mọc lại sau một khoảng thời gian
    private IEnumerator RegrowTree()
    {
        yield return new WaitForSeconds(regrowTime);  // Đợi 10 giây

        // Cập nhật trạng thái cây trở lại như ban đầu
        treeRecord.treeState = 0;

        // Chuyển hình dạng trở lại cây ban đầu
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && treeSprite != null)
        {
            spriteRenderer.sprite = treeSprite;  // Đổi hình dạng thành cây ban đầu
        }

        Debug.Log("Tree has regrown!");
    }
}
