using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class OneTimeCManager : CropManager
{
    void Update()
    {
        if (stage <= stageSprites.Count && fieldInteractable.farmer != null)
        {
            TimeChange();
        }
    }

    #region Grow of ome time tree

    public override void TimeChange()
    {
        if (!isWithered)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (!isLackOfFarmers)
                {
                    if (stage == 2 && isFertilized == false && growthR == 2)
                    {
                        fieldInteractable.Fertilizer.SetActive(true);
                    }
                    else
                    {
                        growthR--;
                    }
                    if (growthR <= 0)
                    {
                        if (stage >= 0 && stage < stageSprites.Count - 1)
                        {
                            if (stage == 2) // Check if fertilizer is needed at stage 3 and 4
                            {
                                if (isFertilized)
                                {
                                    AdvanceStage();
                                }
                                else
                                {
                                    Debug.Log("Fertilizer needed to proceed to the next stage.");
                                    return; // Do not proceed without fertilizer.
                                }
                            }
                            else
                            {
                                AdvanceStage();
                            }
                        }
                        else
                        {
                            AcceptHarvest(true);
                            witherTimer -= toRealSecond;
                            if (witherTimer <= 0)
                            {
                                Wither();
                            }
                        }
                    }
                }
                else
                {
                    witherTimer -= toRealSecond;
                    if (witherTimer <= 0)
                    {
                        Wither();
                    }
                }
                timer = toRealSecond;
            }
        }
        else
        {
            Wither();
        }
    }


    #endregion

    #region One time tree interactable
    public override void HarvestCrop()
    {
        if (isWithered || isHarvest == false)
        {
            return;
        }
        if (stage == stageSprites.Count - 1)
        {
            if (wareHouse.AddWareHouseItem(cropName, itemType , 100, cropIcon))
            {
                Destroy(gameObject);
                //fieldInteractable.crop = null;
            }
            else Debug.Log("inventory full");
        }
    }

    #endregion


}
