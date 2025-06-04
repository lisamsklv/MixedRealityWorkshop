using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Time.timeScale set to: " + Time.timeScale);
        SceneManager.LoadScene(menuSceneName); // ← Load your start menu scene
    }
}
