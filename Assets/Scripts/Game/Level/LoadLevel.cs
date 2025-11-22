using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public int levelToLoad;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LoadSceneWithFade( "Level" + levelToLoad.ToString("D2")));
        }
    }

    // Co routine to load level with fade effect
    IEnumerator LoadSceneWithFade(string sceneName)
    {
        // Start fade out effect
        yield return FadeManager.Instance.FadeOut();
        // Load the new scene
        yield return SceneManager.LoadSceneAsync(sceneName);
    }
}
