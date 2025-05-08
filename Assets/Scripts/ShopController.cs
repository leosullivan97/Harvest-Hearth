using UnityEngine;

public class ShopController : MonoBehaviour
{
    public ShopSeedDisplay[] seeds;
    public ShopCropDisplay[] crops;

    // Toggles the shop UI and updates seed and crop displays when opening
    public void OpenClose()
    {
        if (!UIController.instance.theIC.gameObject.activeSelf)
        {
            gameObject.SetActive(!gameObject.activeSelf);

            if (gameObject.activeSelf)
            {
                foreach (ShopSeedDisplay seed in seeds)
                {
                    seed.UpdateDisplay();
                }

                foreach (ShopCropDisplay crop in crops)
                {
                    crop.UpdateDisplay();
                }
            }
        }
    }
}
