using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCropDisplay : MonoBehaviour
{
    public CropController.CropType crop;

    public Image cropImage;
    public TMP_Text amountText, priceText;

    // Updates the crop icon, quantity, and price in the shop UI
    public void UpdateDisplay()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop);

        cropImage.sprite = info.finalCrop;
        amountText.text = "x" + info.cropAmount;
        priceText.text = "$" + info.cropPrice + " each";
    }

    // Sells all crops of this type, adds money, and refreshes the display
    public void SellCrop()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop);

        if (info.cropAmount > 0)
        {
            CurrencyController.instance.AddMoney(info.cropAmount * info.cropPrice);
            CropController.instance.RemoveCrop(crop);
            UpdateDisplay();
            AudioManager.instance.PlaySFXPitchAdjusted(5);
        }
    }
}
