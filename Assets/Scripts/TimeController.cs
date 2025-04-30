using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public float currentTime;
    public float dayStart, dayEnd;
    public float timeSpeed = 0.25f;

    private bool timeActive;

    public int currentDay = 1;
    public string dayEndScene;

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

    // Initializes the day's starting time and activates the clock
    private void Start()
    {
        currentTime = dayStart;
        timeActive = true;
    }

    // Advances time while active; ends the day when time runs out
    private void Update()
    {
        if (timeActive)
        {
            currentTime += Time.deltaTime * timeSpeed;

            if (currentTime > dayEnd)
            {
                currentTime = dayEnd;
                EndDay();
            }

            if (UIController.instance != null)
            {
                UIController.instance.UpdateTimeText(currentTime);
            }
        }
    }

    // Ends the day, progresses crop growth, and loads the day-end scene
    public void EndDay()
    {
        timeActive = false;
        currentDay++;
        GridInfo.instance.GrowCrop();

        PlayerPrefs.SetString("Transition", "Wake Up");
        SceneManager.LoadScene(dayEndScene);
    }

    // Resets the time and starts a new day
    public void StartDay()
    {
        timeActive = true;
        currentTime = dayStart;
        AudioManager.instance.PlaySFX(6);
    }
}
