using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // All musics
    public AudioClip[] playlist;

    // Component AudioSource
    public AudioSource audioSource;

    // Music index
    private int musicIndex = 0;

    // AudioManager is a singleton
    private static AudioManager instance;


    void Awake()
    {
        // Singleton pattern
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Start the first music
        audioSource.clip = playlist[0];

        // Play the music
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // If no music is playing, start the next one
        if (!audioSource.isPlaying)
        {
            PlaylistSong();
        }
    }

    // Play the next song in the playlist
    void PlaylistSong()
    {
        // Set the next music clip
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }
}
