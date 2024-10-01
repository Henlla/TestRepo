using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    #region Prameter

    [Header("Stage")]
    public int stage; // Trường này lưu trữ giai đoạn hiện tại của cây. Mỗi giai đoạn có thể tương ứng với một sprite khác nhau để hiển thị.
    public Sprite cropIcon; // Biểu tượng đại diện cho cây trong giao diện, có thể sử dụng trong các menu hoặc thông báo.
    public List<Sprite> stageSprites; // Danh sách chứa các sprite tương ứng với các giai đoạn khác nhau của cây (giai đoạn 1, 2, 3,...).

    [Header("Growth time")]
    public int growthS; // Thời gian tối đa cần để cây phát triển từ một giai đoạn đến giai đoạn tiếp theo (thời gian ban đầu).
    public int growthR; // Thời gian còn lại để cây hoàn thành giai đoạn hiện tại.

    [Header("Time Change")]
    public float toRealSecond; // Tỷ lệ chuyển đổi thời gian từ game sang thời gian thực (thời gian thực có thể sử dụng để tính toán các sự kiện).
    protected float timer; // Bộ đếm thời gian để theo dõi thời gian đã trôi qua cho cây.

    [Header("Components")]
    public SpriteRenderer spriteRenderer; // Đối tượng `SpriteRenderer` để hiển thị sprite của cây trên màn hình.
    public FieldInteractable fieldInteractable; // Tham chiếu đến thành phần tương tác với ô đất nơi cây đang trồng (có thể bao gồm các chức năng như thu hoạch hoặc tưới nước).

    [Header("CropStats")]
    public int foodValue; // Giá trị dinh dưỡng của cây, có thể được sử dụng để tính toán các hiệu ứng trong game khi cây được thu hoạch.
    public int nudaCoinValue; // Giá trị của cây dưới dạng tiền tệ trong game, có thể được sử dụng để giao dịch hoặc mua sắm.

    [Header("Wither Time")]
    public float witherTime; // Thời gian cần thiết để cây bị héo sau khi đã trưởng thành (không được chăm sóc hoặc tưới nước).
    public float witherTimer; // Bộ đếm thời gian cho việc theo dõi thời gian còn lại trước khi cây bị héo.

    [Header("Withered Sprite")]
    public Sprite witheredSprite; // Sprite để hiển thị cây khi nó đã héo.
    public bool isWithered = false; // Biến boolean để theo dõi trạng thái héo của cây, cho phép biết liệu cây đã bị héo hay chưa.
    public List<Sprite> stageWitheredSprites;

    [Header("Player")]
    [SerializeField]
    protected WareHouse wareHouse; // Tham chiếu đến đối tượng `PlayerManager`, cho phép truy cập vào thông tin và chức năng của người chơi.
    public string cropName; // Tên của cây, có thể được sử dụng để hiển thị hoặc quản lý trong game.

    [Header("Fertilizer")]
    public bool isFertilized;

    [Header("Farmer")]
    public bool isLackOfFarmers = false;

    [Header("Harvest")]
    public bool isHarvest = false;

    [Header("Type")]
    public ItemType itemType;

    #endregion

    #region Unity method
    void Awake()
    {
        //wareHouse = FindObjectOfType<WareHouse>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        fieldInteractable = GetComponentInParent<FieldInteractable>();
        cropName = gameObject.name.Replace("(Clone)", "").Trim();
        isFertilized = false;

    }

    void Start()
    {
        growthR = growthS;
        timer = toRealSecond;
        witherTimer = witherTime;
        StageChange();
    }



    #endregion

    #region Grow manage
    //Phương thức TimeChange() được sử dụng để tính toán thời gian phát triển của cây và quản lý quá trình chuyển giai đoạn.
    public virtual void TimeChange()
    {
    }

    public void AdvanceStage()
    {
        stage++;
        StageChange();
        growthR = growthS;
        isFertilized = false; // Reset fertilizer status after advancing the stage.
    }

    public void ApplyFertilizer(string cropName)
    {
        ItemData item;
        if (cropName.Equals("OneTime"))
        {
            item = wareHouse.Search("OneTimeFertilizer");
        }
        else
        {
            item = wareHouse.Search("ManyTimeFertilizer");
        }
        if (item.quantity > 0 && item.type == ItemType.Fertilizer)
        {
            item.quantity--;
            isFertilized = true;
            Debug.Log("Fertilizer applied.");
        }
        else
        {
            Debug.Log("Insufficient amount of fertilizer!!!");

        }

    }
    public void AcceptHarvest(bool check)
    {
        if (check)
        {
            //Debug.Log("havest");
            fieldInteractable.HarvestIcon.SetActive(true);

        }
        isHarvest = check;
        //Debug.Log("Can harvest.");
    }

    //Phương thức Wither() được gọi khi cây đã héo.
    public void Wither()
    {
        isWithered = true;
        WitherSprite();
        //fieldInteractable.DeactiveProgressBar();
    }

    //Cập nhật sprite của cây khi nó bị héo.
    public void WitherSprite()
    {
        fieldInteractable.HarvestIcon.SetActive(false);
        spriteRenderer.sprite = stageWitheredSprites[stage];
    }

    //Mục đích: Cập nhật sprite của cây theo giai đoạn hiện tại và cập nhật thanh tiến độ.
    public void StageChange()
    {
        if (!isWithered)
        {

            spriteRenderer.sprite = stageSprites[stage];

        }
    }

    #endregion

    #region Interactable
    public virtual void HarvestCrop()
    {

    }

    #endregion
}
