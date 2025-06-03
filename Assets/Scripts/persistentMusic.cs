using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentMusic : MonoBehaviour
{
    void Awake()
    {
        // Prevent duplicate music players
        if (FindObjectsOfType<PersistentMusic>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "LoseScreen")
        {
            Destroy(gameObject);
        }
    }
}
