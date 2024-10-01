using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "FieldRecord", menuName = "SO/Field/Records")]
public class FieldRecord : ScriptableObject
{
    //int remainingTimeForCurrentStage = cropData.stageTimePassed;


    //public List<FieldData> fieldDatas;
    //public CropLibrary cropLibrary;

    //public FieldData GetFieldData(string name)
    //{
    //    return fieldDatas.Find(f => f.name == name);
    //}
    //public void UpdateAllFields(string lastLogoutTime)
    //{
    //    //get time
    //    TimeSpan timeDifference = DateTime.Now - DateTime.Parse(lastLogoutTime);
    //    int timePassed = (int)timeDifference.TotalSeconds;


    //    foreach (var fieldData in fieldDatas)
    //    {

    //        CropData cropData = fieldData.cropData;
    //        if (cropData == null || string.IsNullOrEmpty(cropData.type)) continue;
    //        GameObject cropPrefab = cropLibrary.GetCrop(cropData.type);
    //        CropManager cropManager = cropPrefab.GetComponent<CropManager>();


    //        while (timePassed > 0 && cropData.stageTimePassed > 0)
    //        {

    //            if (cropData.stage == cropManager.stageSprites.Count - 1 && timePassed >= remainingTimeForCurrentStage)
    //            {

    //                cropData.stage = cropManager.stageSprites.Count - 1;
    //                cropData.stageTimePassed = 0;
    //                timePassed -= remainingTimeForCurrentStage;
    //            }
    //            else if (timePassed >= remainingTimeForCurrentStage)
    //            {

    //                timePassed -= remainingTimeForCurrentStage;
    //                cropData.stage++;
    //                cropData.stageTimePassed = cropManager.growthS;
    //            }
    //            else
    //            {

    //                cropData.stageTimePassed -= timePassed;
    //                timePassed = 0;
    //            }
    //        }

    //        // If the crop has passed all stages, mark it as withered
    //        if (cropData.stage == cropManager.stageSprites.Count - 1 && cropData.stageTimePassed <= 0 && timePassed >= 0)
    //        {

    //            cropData.stage = cropManager.stageSprites.Count - 1;
    //            float witherTimer = cropManager.witherTime + cropData.stageTimePassed - 1;


    //            if (witherTimer <= timePassed)
    //            {
    //                cropData.isWithered = true;
    //                cropManager.witherTimer = witherTimer;
    //                cropData.stageTimePassed = 1 - (int)cropManager.witherTime;
    //            }
    //            else
    //            {
    //                cropData.isWithered = false;
    //            }
    //            Debug.Log("stage cuoi - cropData.isWithered = " + cropData.isWithered);
    //        }

    //    }
    //}
}


public class CropLibrary
{
    public string treeType;
    public string treeName;
    public Sprite icon;
}