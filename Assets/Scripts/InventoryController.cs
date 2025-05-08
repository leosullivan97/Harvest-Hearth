using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public SeedDisplay[] seeds;
    public CropDisplay[] crops;

    // Opens the inventory if the shop is closed; otherwise toggles visibility
    public void OpenClose()
    {
        if (UIController.instance.theShop.gameObject.activeSelf == false)
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
                UpdateDisplay();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Updates all seed and crop display UI elements
    public void UpdateDisplay()
    {
        foreach (SeedDisplay seed in seeds)
        {
            seed.UpdateDisplay();
        }

        foreach (CropDisplay crop in crops)
        {
            crop.UpdateDisplay();
        }
    }
}
