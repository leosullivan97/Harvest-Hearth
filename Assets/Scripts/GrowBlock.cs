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
    public Sprite soilTilled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            SetSoilSprite();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void AdvanceStage()
    {
        currentStage = currentStage + 1;

        if((int)currentStage >= 6)
        {
            currentStage = GrowthStage.barren;
        }
    }

    public void SetSoilSprite()
    {
        if(currentStage == GrowthStage.barren)
        {
            soilSR.sprite = null;
        } else
        {
            soilSR.sprite = soilTilled;
        }
    }

    public void PloughSoil()
    {
        if(currentStage == GrowthStage.barren)
        {
            currentStage = GrowthStage.ploughed;

            SetSoilSprite();
        }
    }
}
