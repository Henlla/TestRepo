using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ManyTimeCManager : CropManager
{
    public int numberOfHarvests;

    void Update()
    {
        if (stage <= stageSprites.Count && fieldInteractable.farmer != null)
        {
            TimeChange();
        }
    }

    #region Grow many time tree
    public override void TimeChange()
    {
        if (!isWithered)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (!isLackOfFarmers)
                {
                    Debug.Log(numberOfHarvests);
                    if ((stage == 2 && isFertilized == false && growthR == 2))
                    {
                        fieldInteractable.Fertilizer.SetActive(true);
                    }
                    else if((stage == 4 && numberOfHarvests == 0 && isFertilized == false))
                    {
                        stage--;
                        fieldInteractable.Fertilizer.SetActive(true);
                        AcceptHarvest(false);
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
                                if (stage == 4) numberOfHarvests = 5;
                                AdvanceStage();
                            }
                        }
                        else
                        {
                            AcceptHarvest(true);
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



    #region Many time tree interactable
    public override void HarvestCrop()
    {
        if (isWithered || isHarvest == false)
        {
            return;
        }
        if (stage == stageSprites.Count - 1)
        {
            if (numberOfHarvests > 0)
            {
                if (wareHouse.AddWareHouseItem(cropName, itemType, 10, cropIcon))
                {
                    numberOfHarvests--;
                    //stage--;
                    StageChange();
                }
                else Debug.Log("inventory full");
            }

        }
    }
    #endregion
}
