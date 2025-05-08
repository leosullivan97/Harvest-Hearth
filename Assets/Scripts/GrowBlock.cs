using UnityEngine;
using UnityEngine.InputSystem;

public class GrowBlock : MonoBehaviour
{
    public enum GrowthStage
    {
        barren,
        ploughed,
        planted,
        growing1,
        growing2,
        ripe
    }

    public GrowthStage currentStage;

    public SpriteRenderer soilSR;
    public Sprite soilTilled, soilWatered;
    public SpriteRenderer cropSR;
    public Sprite cropPlanted, cropGrowing1, cropGrowing2, cropRipe;

    public bool isWatered;
    public bool preventUse;

    private Vector2Int gridPosition;

    public CropController.CropType cropType;
    public float growFailChance;

    // Advances crop stage manually in editor using the N key
    private void Update()
    {
#if UNITY_EDITOR
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            AdvanceCrop();
        }
#endif
    }

    // Moves to the next growth stage or resets to barren after ripe
    public void AdvanceStage()
    {
        currentStage++;
        if ((int)currentStage >= 6)
        {
            currentStage = GrowthStage.barren;
        }
    }

    // Updates the soil sprite based on stage and watering
    public void SetSoilSprite()
    {
        if (currentStage == GrowthStage.barren)
        {
            soilSR.sprite = null;
        }
        else
        {
            soilSR.sprite = isWatered ? soilWatered : soilTilled;
        }

        UpdateGridInfo();
    }

    // Ploughs the soil if it's barren and usable
    public void PloughSoil()
    {
        if (currentStage == GrowthStage.barren && !preventUse)
        {
            currentStage = GrowthStage.ploughed;
            SetSoilSprite();
            AudioManager.instance.PlaySFXPitchAdjusted(4);
        }
    }

    // Waters the soil if allowed
    public void WaterSoil()
    {
        if (!preventUse)
        {
            isWatered = true;
            SetSoilSprite();
            AudioManager.instance.PlaySFXPitchAdjusted(7);
        }
    }

    // Plants a crop if soil is ploughed and watered
    public void PlantCrop(CropController.CropType cropToPlant)
    {
        if (currentStage == GrowthStage.ploughed && isWatered && !preventUse)
        {
            currentStage = GrowthStage.planted;
            cropType = cropToPlant;
            growFailChance = CropController.instance.GetCropInfo(cropType).growthFailChance;
            CropController.instance.UseSeed(cropToPlant);
            UpdateCropSprite();
            AudioManager.instance.PlaySFXPitchAdjusted(3);
        }
    }

    // Updates the crop sprite based on its current growth stage
    public void UpdateCropSprite()
    {
        CropInfo activeCrop = CropController.instance.GetCropInfo(cropType);

        switch (currentStage)
        {
            case GrowthStage.planted:
                cropSR.sprite = activeCrop.planted;
                break;
            case GrowthStage.growing1:
                cropSR.sprite = activeCrop.growStage1;
                break;
            case GrowthStage.growing2:
                cropSR.sprite = activeCrop.growStage2;
                break;
            case GrowthStage.ripe:
                cropSR.sprite = activeCrop.ripe;
                break;
        }

        UpdateGridInfo();
    }

    // Advances crop one stage if watered and usable
    public void AdvanceCrop()
    {
        if (isWatered && !preventUse)
        {
            if (currentStage == GrowthStage.planted || currentStage == GrowthStage.growing1 || currentStage == GrowthStage.growing2)
            {
                currentStage++;
                isWatered = false;
                SetSoilSprite();
                UpdateCropSprite();
            }
        }
    }

    // Harvests the crop if ripe and usable
    public void HarvestCrop()
    {
        if (currentStage == GrowthStage.ripe && !preventUse)
        {
            currentStage = GrowthStage.ploughed;
            SetSoilSprite();
            cropSR.sprite = null;

            CropController.instance.AddCrop(cropType);
            AudioManager.instance.PlaySFXPitchAdjusted(2);
        }
    }

    // Sets this block’s grid position (x, y)
    public void SetGridPosition(int x, int y)
    {
        gridPosition = new Vector2Int(x, y);
    }

    // Syncs this block’s state to the GridInfo save structure
    private void UpdateGridInfo()
    {
        GridInfo.instance.UpdateInfo(this, gridPosition.x, gridPosition.y);
    }
}
