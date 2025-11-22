using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    // Reference to the CanvasGroup component for fading
    public CanvasGroup fadeCanvasGroup;

    // Duration of the fade effect
    public float fadeDuration = 1.5f;

    // Singleton instance 
    public static FadeManager Instance;


    void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            // Set the instance to this object
            Instance = this;
            DontDestroyOnLoad(gameObject); 

            // Detach from parent to persist across scenes
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject); 
        }
        // If an instance already exists, destroy this object
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // We start with a fully opaque canvas
        StartCoroutine(FadeIn());
    }

    // Co routine
    public IEnumerator FadeIn()
    {
        // Start with a fully opaque canvas
        fadeCanvasGroup.alpha = 1f;

        // Gradually fade to transparent
        float t = 0;
        while (t < fadeDuration)
        {
            // Increment time
            t += Time.deltaTime;

            // Update alpha
            fadeCanvasGroup.alpha = 1f - (t / fadeDuration);
            yield return null;
        }

        // Reset fadeCanvasGroup alpha to 0
        fadeCanvasGroup.alpha = 0f;
    }

    public IEnumerator FadeOut()
    {
        // Start with a fully transparent canvas
        fadeCanvasGroup.alpha = 0;

        // Gradually fade to opaque
        float t = 0;
        while (t < fadeDuration)
        {
            // Increment time
            t += Time.deltaTime;

            // Update alpha
            fadeCanvasGroup.alpha = t / fadeDuration;
            yield return null;
        }

        // Reset fadeCanvasGroup alpha to 1
        fadeCanvasGroup.alpha = 1;
    }
}
