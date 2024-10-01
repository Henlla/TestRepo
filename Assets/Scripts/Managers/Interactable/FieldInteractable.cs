using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FieldInteractable : MonoBehaviour
{

    // Tham chiếu đến nông dân (người chơi)
    public PlayerManager farmer;
    // Đối tượng thanh tiến độ
    public GameObject Fertilizer;
    public GameObject HarvestIcon;
    public GameObject crop;
    // Phương thức để cập nhật thanh tiến độ
    #region filed interactabke
    //public void UpdateProgressBar(float progress)
    //{
    //    if (ProgressBarObj != null)
    //    {
    //        Slider slider = ProgressBarObj.GetComponent<Slider>();
    //        if (slider != null)
    //        {
    //            slider.value = Mathf.Clamp01(progress); // Cập nhật thanh tiến độ
    //        }
    //        else
    //        {
    //            Debug.LogError("Slider component not found on progressBarObj.");
    //        }
    //    }
    //}

    // Phương thức để vô hiệu hóa thanh tiến độ
    //public void DeactiveProgressBar()
    //{
    //    // Ẩn đối tượng thanh tiến độ
    //    if (ProgressBarObj != null)
    //    {
    //        ProgressBarObj.SetActive(false);
    //    }
    //}
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<FertilizerManager>())
        {
            Fertilize();
        }
        else if (collider.gameObject.GetComponent<ToolManager>())
        {
            HarvestCrop();
        }

    }
    public void Fertilize()
    {
        if (crop != null)
        {
            CropManager cropManager = crop.GetComponent<CropManager>();

            if (cropManager != null && !cropManager.isFertilized)
            {
                if ((cropManager.stage == 2 && cropManager is OneTimeCManager) ||
                ((cropManager.stage == 2 || cropManager.stage == 4) && cropManager is ManyTimeCManager))
                {
                    if (cropManager is OneTimeCManager)
                    {
                        cropManager.ApplyFertilizer("OneTime");
                    }
                    else
                    {
                        cropManager.ApplyFertilizer("ManyTime");
                    }
                    Fertilizer.SetActive(false);
                   // Debug.Log($"{cropManager.cropName} has been fertilized.");
                }
            }
            else
            {
                //Debug.Log("This tree has been fertilized or is not a valid tree.");
            }
        }
        else
        {
            //Debug.Log("Not found tree to fertilize");
        }
    }

    public void HarvestCrop()
    {
        if (crop != null)
        {
            CropManager cropManager = crop.GetComponent<CropManager>();
            OneTimeCManager oneTimeCManager = crop.GetComponent <OneTimeCManager>();
            ManyTimeCManager manyTimeCManager = crop.GetComponent<ManyTimeCManager>();
            if (cropManager != null && cropManager.isHarvest && (oneTimeCManager != null || manyTimeCManager != null))
            {

                if (oneTimeCManager)
                {
                    oneTimeCManager.HarvestCrop();
                    Destroy(gameObject);
                }
                else
                {
                    manyTimeCManager.HarvestCrop();
                }
            }
            else
            {
                Debug.Log($"{cropManager.cropName} has been harvest.");
            }
        }
        else
        {
            Debug.Log("Not found tree to harvest!!!");
        }
    }
    #endregion

}


