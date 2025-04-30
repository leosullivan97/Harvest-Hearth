using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public GameObject[] toolbarActivatorIcons;
    public TMP_Text timeText;
    public InventoryController theIC;
    public ShopController theShop;
    public Image seedImage;
    public TMP_Text moneyText;
    public GameObject pauseScreen;
    public string mainMenuScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Handles keyboard inputs for UI toggles and pause control
    private void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            theIC.OpenClose();
        }

#if UNITY_EDITOR
        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            theShop.OpenClose();
        }
#endif

        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame)
        {
            PauseUnpause();
        }
    }

    // Activates the toolbar icon for the selected tool
    public void SwitchTool(int selected)
    {
        foreach (GameObject icon in toolbarActivatorIcons)
        {
            icon.SetActive(false);
        }

        toolbarActivatorIcons[selected].SetActive(true);
    }

    // Updates the displayed in-game time text
    public void UpdateTimeText(float currentTime)
    {
        if (currentTime < 12)
        {
            timeText.text = Mathf.FloorToInt(currentTime) + " AM";
        }
        else if (currentTime < 13)
        {
            timeText.text = "12 PM";
        }
        else if (currentTime < 24)
        {
            timeText.text = Mathf.FloorToInt(currentTime - 12) + " PM";
        }
        else if (currentTime < 25)
        {
            timeText.text = "12 AM";
        }
        else
        {
            timeText.text = Mathf.FloorToInt(currentTime - 24) + " AM";
        }
    }

    // Updates the seed image in the UI and plays a sound effect
    public void SwitchSeed(CropController.CropType crop)
    {
        seedImage.sprite = CropController.instance.GetCropInfo(crop).seedType;
        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    // Updates the displayed money text in the UI
    public void UpdateMoneyText(float currentMoney)
    {
        moneyText.text = "$" + currentMoney;
    }

    // Toggles the pause menu and game time scale
    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    // Loads the main menu and destroys persistent gameplay objects
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);

        Destroy(gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GridInfo.instance.gameObject);
        Destroy(TimeController.instance.gameObject);
        Destroy(CropController.instance.gameObject);
        Destroy(CurrencyController.instance.gameObject);

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    // Quits the game and plays a sound effect
    public void QuitGame()
    {
        Application.Quit();
        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }
}
