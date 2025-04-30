using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaSwitcher : MonoBehaviour
{
    public string sceneToLoad;
    public Transform startPoint;
    public string transitionName;

    // Moves the player to the startPoint if returning from another scene with a matching transition name
    private void Start()
    {
        if (PlayerPrefs.HasKey("Transition"))
        {
            if (PlayerPrefs.GetString("Transition") == transitionName)
            {
                PlayerController.instance.transform.position = startPoint.position;
            }
        }
    }

    // Triggers scene transition and stores the transition name when the player enters the collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered.");
            PlayerPrefs.SetString("Transition", transitionName);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
