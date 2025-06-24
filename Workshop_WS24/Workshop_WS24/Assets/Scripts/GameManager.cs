using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

using Unity.XR.CoreUtils;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int customersServedCorrectly = 0;
    public int customersMissedOrIncorrect = 0;
    public float gameDuration = 120f;
    private float timer;

    public GameObject gameOverUI;
    public TMPro.TextMeshProUGUI servedText;
    public TMPro.TextMeshProUGUI missedText;

    public int menuSceneBuildIndex = 0;

    public string menuSceneName = "1 Start Scene"; // Set this in the Inspector or hardcode

    public XROrigin xrOrigin; // Assign in Inspector

    public bool isGameOver = false;

    public CustomerSpawner customerSpawner; // assign this in Inspector


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timer = gameDuration;
        if (gameOverUI != null) gameOverUI.SetActive(false);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            EndGame();
        }
    }

    public void RegisterServed(bool correct)
    {
        if (correct)
            customersServedCorrectly++;
        else
            customersMissedOrIncorrect++;
    }

    public void RotatePlayerToFaceUI()
{
    Transform head = xrOrigin.Camera.transform;
    Vector3 uiPos = gameOverUI.transform.position;

    // Direction from head to UI, ignore Y to rotate flat
    Vector3 flatHeadPos = new Vector3(head.position.x, 0, head.position.z);
    Vector3 flatUIPos = new Vector3(uiPos.x, 0, uiPos.z);

    Vector3 directionToUI = (flatUIPos - flatHeadPos).normalized;

    // Calculate the rotation angle needed
    float angle = Vector3.SignedAngle(xrOrigin.transform.forward, directionToUI, Vector3.up);

    // Rotate the whole rig around the player's head position
    xrOrigin.RotateAroundCameraUsingOriginUp(angle);
}


    void EndGame()
{
    isGameOver = true;

    if (customerSpawner != null)
        customerSpawner.StopSpawning();

    gameOverUI?.SetActive(true);
    RotatePlayerToFaceUI();

    if (servedText != null)
        servedText.text = $"{customersServedCorrectly}";
    if (missedText != null)
        missedText.text = $"{customersMissedOrIncorrect}";
}


    public void BackToMenu()
    {
        //Time.timeScale = 1f;
        Debug.Log("Returning to menu...");
        Debug.Log("Time.timeScale set to: " + Time.timeScale);
        SceneManager.LoadScene(menuSceneName); // ← Load your start menu scene
    }

}
