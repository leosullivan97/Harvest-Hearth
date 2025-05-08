using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSeedDisplay : MonoBehaviour
{
    public CropController.CropType crop;

    public Image seedImage;
    public TMP_Text seedAmount, priceText;

    // Updates the seed icon, quantity, and price in the shop UI
    public void UpdateDisplay()
    {
        CropInfo info = CropController.instance.GetCropInfo(crop);

        seedImage.sprite = info.seedType;
        seedAmount.text = "x" + info.seedAmount;
        priceText.text = "$" + info.seedPrice + " each";
    }

    // Attempts to purchase the given amount of seeds if enough money is available
    public void BuySeed(int amount)
    {
        CropInfo info = CropController.instance.GetCropInfo(crop);

        if (CurrencyController.instance.CheckMoney(info.seedPrice * amount))
        {
            CropController.instance.AddSeed(crop, amount);
            CurrencyController.instance.SpendMoney(info.seedPrice * amount);
            UpdateDisplay();
            AudioManager.instance.PlaySFXPitchAdjusted(5);
        }
    }
}
