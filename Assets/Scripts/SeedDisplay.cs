using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedDisplay : MonoBehaviour
{
    public CropController.CropType crop;
    public Image seedImage;
    public TMP_Text seedAmount;

    // Updates the seed image and amount based on current crop data
    public void UpdateDisplay()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop);
        seedImage.sprite = info.seedType;
        seedAmount.text = "x" + info.seedAmount;
    }

    // Sets the selected seed and closes the inventory UI
    public void SelectSeed()
    {
        PlayerController.instance.SwitchSeed(crop);
        UIController.instance.SwitchSeed(crop);
        UIController.instance.theIC.OpenClose();
    }
}
