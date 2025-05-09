using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource titleMusic;
    public AudioSource[] bgMusic;
    public AudioSource[] sfx;

    private int currentTrack;
    private bool isPaused;

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

    // Initializes currentTrack index
    private void Start()
    {
        currentTrack = -1;
    }

    // Continues playing the next background track if not paused
    private void Update()
    {
        if (!isPaused)
        {
            if (currentTrack == -1)
            {
                return;
            }
            if (!bgMusic[currentTrack].isPlaying)
            {
                PlayNextBGM();
            }
        }
    }

    // Stops all background and title music
    public void StopMusic()
    {
        foreach (AudioSource track in bgMusic)
        {
            track.Stop();
        }

        titleMusic.Stop();
    }

    // Stops all music and plays the title track
    public void PlayTitle()
    {
        StopMusic();
        titleMusic.Play();
    }

    // Stops music and plays the next background track in the array
    public void PlayNextBGM()
    {
        StopMusic();

        currentTrack++;
        if (currentTrack >= bgMusic.Length)
        {
            currentTrack = 0;
        }

        bgMusic[currentTrack].Play();
    }

    // Pauses the currently playing background track
    public void PauseMusic()
    {
        isPaused = true;
        bgMusic[currentTrack].Pause();
    }

    // Resumes the currently paused background track
    public void ResumeMusic()
    {
        isPaused = false;
        bgMusic[currentTrack].Play();
    }

    // Plays the specified sound effect by index
    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    // Randomizes pitch slightly and plays the specified sound effect
    public void PlaySFXPitchAdjusted(int sfxToPlay)
    {
        sfx[sfxToPlay].pitch = Random.Range(0.8f, 1.2f);
        PlaySFX(sfxToPlay);
    }
}
