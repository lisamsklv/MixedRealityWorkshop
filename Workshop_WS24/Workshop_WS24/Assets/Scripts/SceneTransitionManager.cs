using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public static SceneTransitionManager singleton;

    [Header("Start Menu Settings")]
    public Transform menuCameraSpawnPoint;  // Assign in Inspector

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);
            return;
        }

        singleton = this;
        DontDestroyOnLoad(gameObject); // Optional: Persist between scenes
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        SceneManager.LoadScene(sceneIndex);

        yield return null; // Wait for one frame

        if (SceneManager.GetActiveScene().buildIndex == sceneIndex && menuCameraSpawnPoint != null)
        {
            Camera.main.transform.position = menuCameraSpawnPoint.position;
            Camera.main.transform.rotation = menuCameraSpawnPoint.rotation;
        }
    }

    public void GoToSceneAsync(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }

    IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        Debug.Log("Starting async load...");

        if (fadeScreen != null)
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.fadeDuration);
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;

        // Wait until scene is ~90% loaded
        while (operation.progress < 0.9f)
        {
            Debug.Log($"Loading progress: {operation.progress * 100f}%");
            yield return null;
        }

        Debug.Log("Scene loaded. Activating...");
        operation.allowSceneActivation = true;

        // Wait one frame after activation
        yield return null;

        // If menu scene, teleport camera
        if (SceneManager.GetActiveScene().buildIndex == sceneIndex && menuCameraSpawnPoint != null)
        {
            Camera.main.transform.position = menuCameraSpawnPoint.position;
            Camera.main.transform.rotation = menuCameraSpawnPoint.rotation;
        }
    }
}
