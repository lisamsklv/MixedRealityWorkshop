using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.XR.CoreUtils;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int customersServedCorrectly = 0;
    public int customersMissedOrIncorrect = 0;
    public float gameDuration = 120f;
    private float timer;

    public GameObject gameOverUI;

    public TextMeshProUGUI servedText;
    public TextMeshProUGUI missedText;

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

    void FacePlayerTowardGameOverScreen()
{
    var cameraTransform = Camera.main.transform;

    var rig = GameObject.FindObjectOfType<XROrigin>();
    if (rig != null)
    {
        Vector3 directionToUI = gameOverUI.transform.position - rig.transform.position;
        directionToUI.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToUI);

        rig.transform.rotation = targetRotation;
    }
}


    void EndGame()
{
    Time.timeScale = 0f;

    if (gameOverUI != null)
    {
        gameOverUI.SetActive(true);

        if (servedText != null)
            servedText.text = $"{customersServedCorrectly}";
        if (missedText != null)
            missedText.text = $"{customersMissedOrIncorrect}";
    }

    FacePlayerTowardGameOverScreen();
}

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
