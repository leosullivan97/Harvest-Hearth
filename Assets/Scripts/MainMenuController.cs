using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public string levelToStart;

    // Plays the title music when the main menu loads
    private void Start()
    {
        AudioManager.instance.PlayTitle();
    }

    // Starts the game by loading the selected level and stopping the music
    public void PlayGame()
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene(levelToStart);
        // Uncomment for BGM
        //AudioManager.instance.PlayNextBGM();
        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    // Quits the game and plays a sound effect
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game.");
        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }
}
