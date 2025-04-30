using UnityEngine;
using System.Collections.Generic;

public class CropController : MonoBehaviour
{
    public static CropController instance;

    public enum CropType
    {
        pumpkin,
        lettuce,
        carrot,
        hay,
        potato,
        strawberry,
        tomato,
        eggplant
    }

    // List of all crops and their related data
    public List<CropInfo> cropList = new List<CropInfo>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Returns the CropInfo for a specified crop type
    public CropInfo GetCropInfo(CropType cropToGet)
    {
        for (int i = 0; i < cropList.Count; i++)
        {
            if (cropList[i].cropType == cropToGet)
            {
                return cropList[i];
            }
        }

        return null;
    }

    // Decreases the seed count for a specified crop type
    public void UseSeed(CropType seedToUse)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == seedToUse)
            {
                info.seedAmount--;
            }
        }
    }

    // Increases the harvested crop count for a specified crop type
    public void AddCrop(CropType cropToAdd)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == cropToAdd)
            {
                info.cropAmount++;
            }
        }
    }

    // Adds a specific number of seeds to a given crop type
    public void AddSeed(CropType seedToAdd, int amount)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == seedToAdd)
            {
                info.seedAmount += amount;
            }
        }
    }

    // Resets the harvested crop count for a specified crop type
    public void RemoveCrop(CropType cropToRemove)
    {
        foreach (CropInfo info in cropList)
        {
            if (info.cropType == cropToRemove)
            {
                info.cropAmount = 0;
            }
        }
    }
}

[System.Serializable]
public class CropInfo
{
    public CropController.CropType cropType;

    public Sprite finalCrop;
    public Sprite seedType;
    public Sprite planted;
    public Sprite growStage1;
    public Sprite growStage2;
    public Sprite ripe;

    public int seedAmount;
    public int cropAmount;

    [Range(0f, 100f)]
    public float growthFailChance;

    public float seedPrice;
    public float cropPrice;
}
