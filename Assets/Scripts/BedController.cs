using UnityEngine;
using UnityEngine.InputSystem;

public class BedController : MonoBehaviour
{
    private bool canSleep;

    // Ends the day if the player is within range and presses a valid input
    private void Update()
    {
        if (canSleep)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame ||
                Keyboard.current.spaceKey.wasPressedThisFrame ||
                Keyboard.current.eKey.wasPressedThisFrame)
            {
                if (TimeController.instance != null)
                {
                    TimeController.instance.EndDay();
                }
            }
        }
    }

    // Enables sleeping when the player enters the bed's trigger area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSleep = true;
        }
    }

    // Disables sleeping when the player exits the bed's trigger area
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canSleep = false;
        }
    }
}
