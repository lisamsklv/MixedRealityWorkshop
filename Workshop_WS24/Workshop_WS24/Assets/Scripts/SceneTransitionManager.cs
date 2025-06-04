using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    public static SceneTransitionManager singleton;

    [Header("Start Menu Settings")]
    public Transform menuCameraSpawnPoint;  // Assign in Inspector (the spawn point in start menu scene)

    private void Awake()
    {
        if (singleton && singleton != this)
            Destroy(singleton);

        singleton = this;
    }

    public void GoToScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneRoutine(sceneIndex));
    }

    IEnumerator GoToSceneRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(fadeScreen.fadeDuration);

        SceneManager.LoadScene(sceneIndex);

        // Wait a frame for scene to load
        yield return null;

        // If loaded scene is the start menu, reset camera position
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
    Debug.Log("Starting fade and scene load...");
    //fadeScreen.FadeOut();

    AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
    operation.allowSceneActivation = false;

    //float timer = 0;
    while (!operation.isDone)
{
    yield return null;
}


    Debug.Log("Activating scene...");
    operation.allowSceneActivation = true;
}

}
