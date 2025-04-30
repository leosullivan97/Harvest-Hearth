using UnityEngine;
using UnityEngine.InputSystem;

public class ShopActivator : MonoBehaviour
{
    private bool canOpen;

    // Checks for input to open the shop if the player is within range
    private void Update()
    {
        if (canOpen)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (!UIController.instance.theShop.gameObject.activeSelf)
                {
                    UIController.instance.theShop.OpenClose();
                    AudioManager.instance.PlaySFX(0);
                }
            }
        }
    }

    // Enables shop access when the player enters the trigger area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canOpen = true;
        }
    }

    // Disables shop access when the player exits the trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canOpen = false;
        }
    }
}
