using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DayEndController : MonoBehaviour
{
    public TMP_Text dayText;
    public string wakeUpScene;

    // Displays the current day and plays transition audio
    private void Start()
    {
        if (TimeController.instance != null)
        {
            dayText.text = "- Day " + TimeController.instance.currentDay + " -";
        }

        AudioManager.instance.PauseMusic();
        AudioManager.instance.PlaySFX(1);
    }

    // Starts a new day and loads the wake-up scene when any input is pressed
    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            TimeController.instance.StartDay();
            SceneManager.LoadScene(wakeUpScene);
        }
    }
}
